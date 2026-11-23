using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yad_Aloun2.Data;
using Yad_Aloun2.Models;

namespace Yad_Aloun2.Controllers
{
    public class UserController : Controller
    {
        public readonly AppDbContext _DB;
        public UserController(AppDbContext DB)
        {
            _DB = DB;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _DB.Users.ToListAsync();

            return View(data);
        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            // استبعاد الخصائص الملاحية والمعرف التلقائي لمنع فشل الـ ModelState
            ModelState.Remove("HomeProject");
            ModelState.Remove("User_Id");

            if (ModelState.IsValid)
            {
                try
                {
                    _DB.Users.Add(user);

                    await _DB.SaveChangesAsync();



                    // تخزين معرف المستخدم في السيشن

                    HttpContext.Session.SetInt32("UserId", user.User_Id);



                    // التعديل: التوجيه المباشر والقاطع لصفحة الملف الشخصي للمستخدم لمنع تكرار الفورم

                    return RedirectToAction("Profile", "User");

                }
                catch (Exception ex)
                {
                    var databaseError = ex.InnerException?.Message ?? ex.Message;

                    ModelState.AddModelError(string.Empty, "خطأ في قاعدة البيانات: " + databaseError);

                    return View(user);
                }
            }

            // إذا وجد الـ C# أي حقل ناقص أو غير صالح، ستظهر الأخطاء هنا رغماً عنها
            foreach (var modelStateKey in ModelState.Keys)
            {
                var value = ModelState[modelStateKey];
                foreach (var error in value.Errors)
                {
                    ModelState.AddModelError(string.Empty, $"الحقل [{modelStateKey}] يحتوي على خطأ: {error.ErrorMessage}");
                }
            }

            return View(user);
        }
        public async Task<IActionResult> Inquiry(Inquiry inquiry)
        {
            _DB.Inquiry.Add(inquiry);
            await _DB.SaveChangesAsync();

            return View();
        }
        [HttpGet]
        public IActionResult Profile()
        {

            return View();
        }
        [HttpPost]

        public async Task<ActionResult> Profile(User model)
        {
            if (ModelState.IsValid)
            {
                var userInDb = await _DB.Users.FindAsync(model.User_Id);
                if (userInDb != null)
                {
                    userInDb.FullName = model.FullName;
                    userInDb.Email = model.Email;
                    userInDb.Phone = model.Phone;
                    userInDb.FamilyPhoneNumber = model.FamilyPhoneNumber;
                    userInDb.DisabilityType = model.DisabilityType;
                    userInDb.Age = model.Age;
                    userInDb.location = model.location;

                    await _DB.SaveChangesAsync();
                    return RedirectToAction("Profile");
                }

            }
            return View("Profile", model);
        }


        [HttpGet]

        public IActionResult SignIn()

        {

            return View();

        }
        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SignIn(string email, string phone)

        {

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone))

            {

                TempData["LoginError"] = "يرجى إدخال البريد الإلكتروني ورقم الهاتف.";

                // إرجاع المستخدم لنفس الصفحة عبر الـ URL لضمان عدم ظهور صفحة الإيرور

                return Redirect(Request.Headers["Referer"].ToString() ?? "~/User/Register");

            }



            // البحث عن المستخدم الذي يطابق الإيميل ورقم الهاتف معاً

            var user = await _DB.Users.FirstOrDefaultAsync(u => u.Email == email && u.Phone == phone);



            if (user != null)

            {

                // تخزين معرف المستخدم في السيشن لتسجيل دخوله

                HttpContext.Session.SetInt32("UserId", user.User_Id);



                var returnUrl = Request.Query["returnUrl"].ToString();

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))

                    return Redirect(returnUrl);



                return RedirectToAction("Index", "Home", new { area = "" });

            }



            // في حال كانت البيانات خاطئة، نضع الرسالة في TempData لتقرأها الصفحة الخضراء

            TempData["LoginError"] = "البريد الإلكتروني أو رقم الهاتف غير متطابق مع أي حساب مسجل لدينا.";



            // إرجاع المستخدم تلقائياً لنفس الصفحة المعروضة حالياً

            return Redirect(Request.Headers["Referer"].ToString() ?? "~/User/Register");

        }

    }

}


    
