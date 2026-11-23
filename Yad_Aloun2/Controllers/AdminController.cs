using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yad_Aloun2.Data;
using Yad_Aloun2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Yad_Aloun2.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _DB;
        public AdminController(AppDbContext DB)
        {
            _DB = DB;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _DB.Admins.Include(a => a.Inquiries).ToListAsync();

            return View(data);
        }
        [HttpGet]//هاد بسجل دخول المسؤول
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var admin = _DB.Admins.FirstOrDefault(a => a.Email == email && a.AdminPassword == password);

            if (admin == null)
            {
                ModelState.AddModelError("", "البريد الإلكتروني أو كلمة المرور غير صحيحة");
                return View();
            }

            // التحقق من وجود مسؤول آخر مسجل دخوله
            if (_DB.Admins.Any(a => a.IsLoggedIn && a.Admin_Id != admin.Admin_Id))
            {
                ModelState.AddModelError("", "يوجد مسؤول آخر مسجل دخوله حالياً.");
                return View();
            }

            admin.IsLoggedIn = true;
            _DB.SaveChanges();

            HttpContext.Session.SetInt32("AdminId", admin.Admin_Id);
            HttpContext.Session.SetString("Role", "Admin");

            return RedirectToAction(nameof(HomeAdmin));
        }

        public IActionResult Logout()
        {
            int? adminId = HttpContext.Session.GetInt32("AdminId");
            if (adminId != null)
            {
                var admin = _DB.Admins.Find(adminId);
                if (admin != null)
                {
                    admin.IsLoggedIn = false;
                    _DB.SaveChanges();
                }
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult CreateAdmin()
        {
            ViewBag.AdminExists = _DB.Admins.Any();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAdmin(Admin admin)
        {
            if (_DB.Admins.Any())
            {
                ModelState.AddModelError("", "تم إنشاء حساب المسؤول مسبقاً");
                return View(admin);
            }

            if (ModelState.IsValid)
            {
                _DB.Admins.Add(admin);
                await _DB.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            return View(admin);
        }

        

        public IActionResult HomeAdmin()
        {
            string role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            int? adminId = HttpContext.Session.GetInt32("AdminId");

            if (adminId == null)
            {
                return RedirectToAction("Login");
            }

            var admin = _DB.Admins.Find(adminId);

            return View(admin);
        }
        

        [HttpGet]
        public IActionResult Overview()
        {
            string role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> CentersAndServicesManagement()
        {
            string role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            var centers = await _DB.Center.ToListAsync();
            return View(centers);
        }
        public IActionResult Create()
        {
            string role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(Center center)
        {
            if (ModelState.IsValid)
            {
                int? adminId = HttpContext.Session.GetInt32("AdminId");

                center.Admin_Id = adminId.Value;

                _DB.Center.Add(center);
                await _DB.SaveChangesAsync();

                return RedirectToAction("CentersAndServicesManagement");
            }

            return View(center);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CentersAndServicesManagement(Center center)
        {
            _DB.Center.Add(center);
            await _DB.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var center = await _DB.Center.FindAsync(id);
            if (center == null)
            {
                return NotFound();
            }
            return View(center);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var center = await _DB.Center.FindAsync(id);
            if (center == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _DB.Update(center);
                await _DB.SaveChangesAsync();
                return RedirectToAction(nameof(CentersAndServicesManagement));
            }

            return View(center);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var Center = await _DB.Center.FindAsync(id);
            if (Center == null)

            {
                return NotFound();
            }
            return View(Center);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Center = await _DB.Center.FindAsync(id);
            if (Center != null)
            {
                _DB.Center.Remove(Center);
                await _DB.SaveChangesAsync();
            }

            return RedirectToAction(nameof(CentersAndServicesManagement));
        }




        [HttpGet]
        public IActionResult RequestsForAssistance()
        {
            string role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            var assistanceRequests = _DB.Inquiry.ToList();
            return View(assistanceRequests);
        }

        [HttpGet]
        public async Task<IActionResult> CompanyFiles()
        {
            string role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            var CompanyFiles = await _DB.Company_Files.ToListAsync();
            return View(CompanyFiles);
        }
        [HttpGet]
        public async Task<IActionResult> UserFiles()
        {
            string role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            var userFiles = await _DB.UserFile.ToListAsync();
            return View(userFiles);
        }

        [HttpGet]
        public IActionResult add_newJob()
        {
            var jobs = _DB.JobOpportunity.ToList();

            return View(jobs);
        }
        public IActionResult AddJob()
        {
            ViewBag.Companies = _DB.Donor_Company.ToList();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddJob(JobOpportunity job)
        {
            _DB.JobOpportunity.Add(job);

            await _DB.SaveChangesAsync();

            return RedirectToAction("add_newJob");
        }
        [HttpGet]
        public async Task<IActionResult> EditJob(int id)
        {
            var job = await _DB.JobOpportunity.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }

           

            return View(job);
        }
        [HttpPost]
        public async Task<IActionResult> EditJob(JobOpportunity job)
        {
            _DB.JobOpportunity.Update(job);

            await _DB.SaveChangesAsync();

            return RedirectToAction("add_newJob");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _DB.JobOpportunity.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        [HttpPost, ActionName("DeleteJob")]
        public async Task<IActionResult> DeleteJobConfirmed(int id)
        {
            var job = await _DB.JobOpportunity.FindAsync(id);

            if (job != null)
            {
                _DB.JobOpportunity.Remove(job);
                await _DB.SaveChangesAsync();
            }

            return RedirectToAction("add_newJob");
        }
        [HttpPost]
        public IActionResult ReplyInquiry(int id, string reply)
        {
            var inquiry = _DB.Inquiry.Find(id);

            if (inquiry != null)
            {
                inquiry.AdminReply = reply;
                _DB.SaveChanges();
            }

            return RedirectToAction("RequestsForAssistance");
        }
    }
}
    