using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Yad_Aloun2;
using Yad_Aloun2.Data;
using Yad_Aloun2.Models;          // تأكدي أن هذا اسم النيم-سبيس الصحيح لموديلاتك

namespace Yad_Aloun2.Controllers
{
    public class services : Controller
    {
        // 1. تهيئة قاعدة البيانات داخل الكنترولر
        private readonly AppDbContext _DB;
        public services(AppDbContext DB)
        {
            _DB = DB;
        }

        // 2. الصفحة الرئيسية للخدمات
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ReadMore()
        {
            return View();
        }

        // 3. صفحة طلب المساعدة (GET) -> تضعين الكود هنا

        public async Task<IActionResult> ReviewOrders()
        {
            // 1. التحقق من جلسة المستخدم
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "user"); // تأكد من اسم الكنترولر (Account أو User)

            // 2. جلب طلبات المساعدة الخاصة بهذا المستخدم فقط
            var myRequests = await _DB.AssistanceRequest
                .Where(r => r.User_Id == userId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            // 3. جلب استفسارات هذا المستخدم فقط (تصحيح هام)
            var myInquiries = await _DB.Inquiry
                .Where(i => i.User_Id == userId) // افترضت أن الجدول يحتوي على UserId
                .ToListAsync();

            ViewBag.Inquiries = myInquiries;

            // 4. تمرير قائمة الطلبات
            return View(myRequests);
        }


        [HttpGet]
        public async Task<IActionResult> RequestAssistance()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            // التأكد من جلب الخدمات مع جدول الشركات المرتبط بها
            var availableServices = await _DB.ServiceItems
      .Where(s => s.Available)
      .Include(s => s.Donor_Company)
      .ToListAsync();

            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }
            // هذا السطر هو الذي يمنع الأيرور في المتصفح
            ViewBag.AvailableServices = availableServices;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestAssistance(AssistanceRequest model, IFormFile? file)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "User");
            model.User_Id = userId.Value;

            // 1. معالجة الملف (قيمة نصية لتجنب NULL)
            model.AttachmentUrl = (file != null && file.Length > 0) ? "/uploads/" + Guid.NewGuid() + Path.GetExtension(file.FileName) : "No-File";

            // 2. تعيين الشركة (hayat) إجبارياً
            model.Company_ID = 2;

            // 3. القيم التلقائية
            model.CreatedAt = DateTime.Now;
            model.Status = "Pending";

            _DB.AssistanceRequest.Add(model);
            await _DB.SaveChangesAsync(); // الآن يجب أن يعمل بدون أخطاء FOREIGN KEY

            TempData["SuccessMessage"] = "تم إرسال الطلب بنجاح";
            return RedirectToAction("ReviewOrders");
        }
        // بقية صفحات الأكشن الخاصة بكِ...
        public IActionResult jobs()

        {
            return View();
        }
        public IActionResult ServerMap()
        {
            return View();
        }

       
        public IActionResult FamilySupport()
        { return View();
        }

        public IActionResult Support()
        { return View();
        }
        [HttpPost]

        public async Task<IActionResult> SendInquiry(Inquiry inquiry)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("SignIn", "User");
            }

            inquiry.User_Id = userId.Value;
            inquiry.AdminReply = "";

            _DB.Inquiry.Add(inquiry);
            await _DB.SaveChangesAsync();

            TempData["Success"] = "تم إرسال استفسارك بنجاح";
            return RedirectToAction("Support");
        }
    

        public IActionResult HelpRequsets() 
        { 
            return View();
        }

        // أضيفي هذا الأكشن بدلاً من HomeProjects البسيط
        public IActionResult HomeProject()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId != null)
            {
                var user = _DB.Users.FirstOrDefault(u => u.User_Id == userId);

                ViewBag.UserFullName = user?.FullName;
                ViewBag.UserPhone = user?.Phone;
                ViewBag.IsLoggedIn = true;
            }
            else
            {
                ViewBag.IsLoggedIn = false;
            }

            return View();
        }

        // ---------------- HOME PROJECTS (WITH DATA) ----------------
        [HttpGet]
        public async Task<IActionResult> HomeProjects()
        {
            var projects = await _DB.HomeProject.ToListAsync();

            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId != null)
            {
                var user = await _DB.Users.FirstOrDefaultAsync(u => u.User_Id == userId);

                ViewBag.UserFullName = user?.FullName;
                ViewBag.UserPhone = user?.Phone;
                ViewBag.IsLoggedIn = true;

                // الأسطر الجديدة: تمرير رقم الـ ID للمستخدم الحالي إلى الفيو
                ViewBag.CurrentUserId = userId;
            }
            else
            {
                ViewBag.IsLoggedIn = false;
                ViewBag.CurrentUserId = null;
            }

            return View(projects ?? new List<HomeProject>());
        }


        // ---------------- CREATE PROJECT ----------------
        [HttpPost]
        public async Task<IActionResult> CreateProject(HomeProject model, IFormFile imageFile)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                TempData["LoginRequired"] = "يجب تسجيل الدخول أولاً لرفع مشروع.";
                return RedirectToAction("Login", "User");
            }
            model.UserId = userId.Value;

            if (imageFile != null && imageFile.Length > 0)
            {
                string uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                string fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                string path = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                model.ImagePath = "/uploads/" + fileName;
            }

            _DB.HomeProject.Add(model);
            await _DB.SaveChangesAsync();

            return RedirectToAction(nameof(HomeProjects));
        }

        // ---------------- EDIT PROJECT (GET) ----------------
        [HttpGet]
        public async Task<IActionResult> EditProject(int id)
        {
            var project = await _DB.HomeProject.FindAsync(id);

            if (project == null)
                return NotFound();

            return View(project);
        }

        // ---------------- EDIT PROJECT (POST) ----------------
        [HttpPost]
        public async Task<IActionResult> EditProject(HomeProject model, IFormFile imageFile)
        {
            var project = await _DB.HomeProject.FindAsync(model.Id);

            if (project == null)
                return NotFound();

            project.ProjectName = model.ProjectName;
            project.OwnerName = model.OwnerName;
            project.Description = model.Description;
            project.Price = model.Price;
            project.Category = model.Category;
            project.ContactNumber = model.ContactNumber;

            if (imageFile != null && imageFile.Length > 0)
            {
                string uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                string fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                string path = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                project.ImagePath = "/uploads/" + fileName;
            }

            await _DB.SaveChangesAsync();

            return RedirectToAction(nameof(HomeProjects));
        }

        // ---------------- DELETE ----------------
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _DB.HomeProject.FindAsync(id);

            if (project != null)
            {
                _DB.HomeProject.Remove(project);
                await _DB.SaveChangesAsync();
            }

            return RedirectToAction(nameof(HomeProjects));
        }
        
    
     



        // أكشن جديد: يتحقق هل المستخدم مسجل أم لا عند الضغط على الزر
        [HttpGet]
        public IActionResult CheckLogin()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                // إذا لم يسجل → نرجع JSON يطلب التحويل
                return Json(new { loggedIn = false });
            }
            var user = _DB.Users.FirstOrDefault(u => u.User_Id == userId);
            return Json(new
            {
                loggedIn = true,
                fullName = user?.FullName,
                phone = user?.Phone
            });
        }
    }
}