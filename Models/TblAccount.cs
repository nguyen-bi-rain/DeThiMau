using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VuTheNguyen_211201947.Models
{
    public partial class TblAccount
    {
        public int Uid { get; set; }
        public string? Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password doesn't match")]
        public string? ConfirmPassword { get; set; }
    }
}
