using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VuTheNguyen_211201947.Models;

namespace VuTheNguyen_211201947.Controllers
{
    public class HangHoasController : Controller
    {
        QLHangHoaContext _context = new QLHangHoaContext();

        // GET: HangHoas
        public async Task<IActionResult> Index()
        {
            var qLHangHoaContext = _context.HangHoas.Include(h => h.MaLoaiNavigation);
            return View(await qLHangHoaContext.ToListAsync());
        }

        
        // GET: HangHoas/Create
        public IActionResult Create()
        {
            ViewData["MaLoai"] = new SelectList(_context.LoaiHangs, "MaLoai", "TenLoai");
            return View();
        }

        // POST: HangHoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoai","TenHang","Gia","Anh")] HangHoa hangHoa)
        {
            
           
            if (ModelState.IsValid)
            {
                _context.Add(hangHoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.LoaiHangs, "MaLoai", "TenLoai", hangHoa.MaLoai);
            return View(hangHoa);
        }

        
        private bool HangHoaExists(int id)
        {
            QLHangHoaContext _context = new QLHangHoaContext();
            return (_context.HangHoas?.Any(e => e.MaHang == id)).GetValueOrDefault();
        }
    }
}
