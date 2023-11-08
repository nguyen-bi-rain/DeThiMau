using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using VuTheNguyen_211201947.Models;

namespace VuTheNguyen_211201947.ViewComponents
{
	public class LoaiHangViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			QLHangHoaContext db = new QLHangHoaContext();
			var loaihang = db.LoaiHangs.ToList();
			return View(loaihang);
		}
	}
}
