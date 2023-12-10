using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VuTheNguyen_211201947.Models
{
    public partial class HangHoa
    {
        [Key]
        public int? MaHang { get; set; }
        [Required]
        public int? MaLoai { get; set; }
        [Required]
        public string? TenHang { get; set; } = null!;
        [Range(100,5000,ErrorMessage = "Gia phai nam trong khoang 100 - 5000")]
        [RegularExpression("^\\d+$")]
        public decimal? Gia { get; set; }
        [RegularExpression("/.(jpg|png|gif|tiff)$")]
        [Required]
        public string? Anh { get; set; }
        
        public virtual LoaiHang? MaLoaiNavigation { get; set; } = null!;
    }
}
