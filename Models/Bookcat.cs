using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace bookecommercewebsite.Models
{
    public partial class Bookcat
    {
        public int Bookcatid { get; set; }

        [Required]
        [Display(Name = "Book Category:")]
        public string Bookcatname { get; set; }
    }
}
