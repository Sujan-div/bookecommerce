using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bookecommercewebsite.Models
{
    public class Contact
    {
        public int Contactid { get; set; }

        [Required]
        [Display(Name = "Name:")]
        public string Contactname { get; set; }

        [Required]
        [Display(Name = "E-mail:")]
        public string Contactemail { get; set; }

        [Required]
        [Display(Name = "Subject:")]
        public string Contactsubject { get; set; }

        [Required]
        [Display(Name = "Notes:")]
        public string Contactnotes { get; set; }
    }
}
