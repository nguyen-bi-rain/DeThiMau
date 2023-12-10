using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using VuTheNguyen_211201947.Models;

namespace VuTheNguyen_211201947.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        private int pageSize = 4;
        public IActionResult Index(int? id)
        {
            QLHangHoaContext db = new QLHangHoaContext();
            var hanghoa = (IQueryable<HangHoa>)db.HangHoas.Include(l => l.MaLoaiNavigation).Where(p => p.Gia >= 100);
            if(id != null)
            {
                hanghoa = (IQueryable<HangHoa>)db.HangHoas.Include(l => l.MaLoaiNavigation).Where(p => p.MaLoai == id);
            }
            int pageNum = (int)Math.Ceiling(hanghoa.Count() / (float)pageSize);
            ViewBag.PageNum = pageNum;
            var res = hanghoa.Take(pageSize).ToList();
            return View(res);
        }
        public IActionResult HangHoaTheoLoai(int? maloaihang)
        {
            QLHangHoaContext db = new QLHangHoaContext();
            var hanghoa = db.HangHoas.Where(p => p.MaLoai == maloaihang).ToList();
            return PartialView("MatHang", hanghoa);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(TblAccount user)
        {
            QLHangHoaContext db = new QLHangHoaContext();
            user.Uid = 1;
            
                var data = db.TblAccounts.Where(a => a.Username.Equals(user.Username) && a.Password.Equals(user.Password)).FirstOrDefault();
                if (data != null)
                {
                    HttpContext.Session.SetString("UserId", data.Uid.ToString());
                    HttpContext.Session.SetString("UserName", data.Username);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(user);
                }
            
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(TblAccount user)
        {
            QLHangHoaContext db = new QLHangHoaContext();
            if (ModelState.IsValid)
            {
                var ac = db.TblAccounts.FirstOrDefault(a => a.Username ==  user.Username);  
                if(ac == null)
                {
                    db.TblAccounts.Add(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ViewBag.error = "User Already exist";
                    return View(user);
                }
            }
            return View(user);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Filter(int? id, int? pageIndex)
        {
            QLHangHoaContext db = new QLHangHoaContext();
            var learners = (IQueryable<HangHoa>)db.HangHoas.Include(m => m.MaLoaiNavigation);
            int page = (int)(pageIndex == null || pageIndex <= 0 ? 1 : pageIndex);
            if (id != null)
            {
                learners = learners.Where(m => m.MaLoai == id);
                ViewBag.Mid = id;
            }
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
            ViewBag.PageNum = pageNum;
            var res = learners.Skip(pageSize * (page - 1)).Take(pageSize).Include(m => m.MaLoaiNavigation);
            return PartialView("MatHang", res);

        }
    }
}