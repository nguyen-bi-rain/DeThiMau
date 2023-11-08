using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            QLHangHoaContext db = new QLHangHoaContext();
            var  hanghoa = db.HangHoas.Where(p => p.Gia >= 100  ).ToList();
            return View(hanghoa);
        }
        public IActionResult HangHoaTheoLoai(int? maloaihang)
        {
			QLHangHoaContext db = new QLHangHoaContext();
            var hanghoa = db.HangHoas.Where(p => p.MaLoai == maloaihang).ToList();
			return PartialView("MatHang",hanghoa);
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
    }
}