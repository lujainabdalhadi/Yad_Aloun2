
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yad_Aloun2.Data;
using Yad_Aloun2.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Yad_Aloun2.Controllers
{
    public class CompanyController : Controller
    {
        private readonly AppDbContext _DB;
        public CompanyController(AppDbContext DB)
        {
            _DB = DB;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _DB.Donor_Company.ToListAsync();

            return View(data);
        }
        [HttpGet]

        public IActionResult LoginCompany()

        {

            return View();

        }

        // 2. استقبال بيانات تسجيل الدخول والتحقق منها (POST)

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> LoginCompany(string email, string phone)

        {

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone))

            {

                TempData["CompanyLoginError"] = "يرجى إدخال البريد الإلكتروني ورقم هاتف الشركة.";

                return Redirect(Request.Headers["Referer"].ToString() ?? "~/Company/Register");

            }

            // البحث عن الشركة التي تطابق البريد الإلكتروني ورقم الهاتف معاً (بدون باسورد)

            var company = await _DB.Donor_Company

                .FirstOrDefaultAsync(c => c.Email == email && c.Phone == phone);

            if (company != null)

            {

                // تخزين معرف الشركة في السيشن لتسجيل دخولها

                HttpContext.Session.SetInt32("CompanyId", company.Company_ID);

                // 🎯 التعديل الرئيسي هنا: التوجيه لصفحة الـ Index الخاصة بنفس هذا الكنترولر (شاشة الشركة)

                return RedirectToAction("HomeCompany", "Company");

            }

            // في حال كانت البيانات خاطئة

            TempData["CompanyLoginError"] = "البريد الإلكتروني أو رقم الهاتف غير متطابق مع أي شركة مسجلة لدينا.";

            // إعادة التوجيه التلقائي لنفس الصفحة لمنع حدوث صفحة الخطأ وثبات اللون الأخضر

            return Redirect(Request.Headers["Referer"].ToString() ?? "~/Company/Register");

        }

        // 3. ميثود إضافية لتسجيل الخروج للشركة

        [HttpPost]

        [ValidateAntiForgeryToken]

        public IActionResult LogoutCompany()

        {

            HttpContext.Session.Remove("CompanyId"); // مسح جلسة الشركة فقط

            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // 2. استقبال بيانات التسجيل ومعالجتها وحفظها
        [HttpPost]
        public async Task<IActionResult> Register(Donor_Company model)
        {
            // أضف هذا السطر لإزالة الحقل من التحقق قبل الفحص
            ModelState.Remove("RequestsAssistance");

            if (ModelState.IsValid)
            {
                _DB.Donor_Company.Add(model);
                await _DB.SaveChangesAsync();

                HttpContext.Session.SetInt32("CompanyId", model.Company_ID);
                return RedirectToAction(nameof(HomeCompany));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult HomeCompany()
        {
           
            int? companyId = HttpContext.Session.GetInt32("CompanyId");

            if (companyId == null)
            {
                return RedirectToAction("LoginCompany");
            }

            var company = _DB.Donor_Company.Find(companyId);

            if (company == null)
            {
                return RedirectToAction("LoginCompany");
            }

            return View(company);
        }
        public IActionResult Statistics()
        {
            return View();
        }
        public async Task<IActionResult>  ManageServices()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId");

            if (companyId == null)
                return RedirectToAction("LoginCompany");

            var services = await _DB.ServiceItems
     .Where(x => x.CompanyId == companyId)
     .ToListAsync();

            return View(services);
        }
        // صفحة إضافة خدمة
        [HttpGet]
        public IActionResult CreateService()
        {
            return View();
        }

        // حفظ خدمة جديدة
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateService(ServiceItem model)
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId");

            if (companyId == null)
                return RedirectToAction("LoginCompany");

            if (ModelState.IsValid)
            {
                model.CompanyId = companyId.Value;

                _DB.ServiceItems.Add(model);
                await _DB.SaveChangesAsync();

                return RedirectToAction(nameof(ManageServices));
            }

            return View(model);
        }

        // صفحة تعديل
        [HttpGet]
        public async Task<IActionResult> EditService(int id)
        {
            var service = await _DB.ServiceItems.FindAsync(id);
            if (service == null) return NotFound();

            return View(service);
        }

        // حفظ التعديل
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(ServiceItem model)
        {
            var service = await _DB.ServiceItems.FindAsync(model.Id);

            if (service == null)
                return NotFound();

            service.Name = model.Name;
            service.Category = model.Category;
            service.Usage = model.Usage;
            service.Available = model.Available;

            await _DB.SaveChangesAsync();

            return RedirectToAction(nameof(ManageServices));
        }

        // حذف خدمة
        [HttpGet]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _DB.ServiceItems.FindAsync(id);

            if (service != null)
            {
                _DB.ServiceItems.Remove(service);
                await _DB.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ManageServices));
        }
       

       
        public IActionResult Usage()
        {
            var data = _DB.usaglog.ToList();

            return View(data);
        }
        public IActionResult Profile()
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyId");

            var company = _DB.Donor_Company
                             .FirstOrDefault(x => x.Company_ID == companyId);

            return View(company);
        }

        [HttpGet]
        public async Task<IActionResult> RequestAssistance()
        {
            // جلب الـ ID الخاص بالشركة الحالية المسجلة دخولها من الـ Session
            int? companyId = HttpContext.Session.GetInt32("CompanyId");

            // إذا لم تكن شركة مسجلة دخولها، يتم توجيهها لصفحة تسجيل الدخول الخاصة بالشركات
            if (companyId == null)
                return RedirectToAction("LoginCompany");

            // 🎯 التصفية الصارمة: جلب الطلبات الموجهة لـ AssignedCompanyId الخاص بالشركة الحالية
            var requests = await _DB.AssistanceRequest
                .Where(x => x.AssignedCompanyId == companyId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return View(requests);
        }

        // 2️⃣ ميثود الاستقبال والحفظ (تبحث ديناميكياً عن الشركة المناسبة في قاعدة البيانات)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestAssistance(AssistanceRequest model, IFormFile? file)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            // إذا لم يكن هناك مستخدم مسجل (لغايات الاختبار والتسهيل سنضع قيمة افتراضية 1 إن لم يوجد)
            if (userId == null)
            {
                userId = 1;
            }

            model.User_Id = userId.Value;
            model.CreatedAt = DateTime.Now;
            model.Status = "Pending";

            // 🎯 الحل الديناميكي: البحث في جدول الشركات عن الشركة التي تطابق فئة الخدمة
            if (!string.IsNullOrEmpty(model.Category))
            {
                Donor_Company targetCompany = null;

                if (model.Category.Contains("كراسي") || model.Category.Contains("روبوتية") || model.Category.Contains("علاج"))
                {
                    // البحث عن أول شركة تحتوي خدماتها على كلمة "طبية" أو تحمل الرمز "1"
                    targetCompany = await _DB.Donor_Company
                        .FirstOrDefaultAsync(c => c.SelectedServices.Contains("طبية") || c.SelectedServices.Contains("1"));
                }
                else if (model.Category.Contains("مالية") || model.Category.Contains("دعم معيشي"))
                {
                    // البحث عن أول شركة تحتوي خدماتها على كلمة "مالية" أو تحمل الرمز "2"
                    targetCompany = await _DB.Donor_Company
                        .FirstOrDefaultAsync(c => c.SelectedServices.Contains("مالية") || c.SelectedServices.Contains("2"));
                }
                else if (model.Category.Contains("أقساط") || model.Category.Contains("تعليمية"))
                {
                    // البحث عن أول شركة تحتوي خدماتها على كلمة "تعليمية" أو تحمل الرمز "3"
                    targetCompany = await _DB.Donor_Company
                        .FirstOrDefaultAsync(c => c.SelectedServices.Contains("تعليمية") || c.SelectedServices.Contains("3"));
                }

                // إذا عثرنا على الشركة المناسبة في قاعدة البيانات، نربط الطلب بـ ID الخاص بها فوراً
                if (targetCompany != null)
                {
                    model.AssignedCompanyId = targetCompany.Company_ID;
                }
            }

            // إزالة الحقول التي تسبب مشاكل الـ Validation
            ModelState.Remove("AttachmentUrl");
            ModelState.Remove("UserId");
            ModelState.Remove("Status");
            ModelState.Remove("CreatedAt");
            ModelState.Remove("AssignedCompanyId");

            if (!ModelState.IsValid)
                return View(model);

            // رفع الملف
            if (file != null && file.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadsFolder);

                string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                model.AttachmentUrl = "/uploads/" + fileName;
            }
            else
            {
                model.AttachmentUrl = "No-File";
            }

            try
            {
                _DB.AssistanceRequest.Add(model);
                await _DB.SaveChangesAsync();

                TempData["SuccessMessage"] = "تم إرسال الطلب بنجاح وتوجيهه للجهة المختصة.";

                // التوجيه لصفحة الطلبات الخاصة بالمستفيد
                return RedirectToAction("MyRequests", "Services");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ReplyRequest()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReplyRequest(int id, string notes)
        {
            var request = await _DB.AssistanceRequest.FindAsync(id);
            if (request == null) return NotFound();

            request.AdminNotes = notes;
            request.Status = "Completed"; // تأكد أن الحالة تظهر للمستخدم

            _DB.AssistanceRequest.Update(request);
            await _DB.SaveChangesAsync();

            return RedirectToAction("RequestAssistance");
        }
    }
     



    }

