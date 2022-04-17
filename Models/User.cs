using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace bookecommercewebsite.Models
{
    public partial class User
    {
        public int Userid { get; set; }

        [Required]
        [Display(Name = "Username:")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Address:")]
        public string Useraddress { get; set; }

        [Required]
        [Display(Name = "E-mail:")]
        public string Useremail { get; set; }

        [Required]
        [Display(Name = "Contact:")]
        public string Usercontact { get; set; }

        [Required]
        [Display(Name = "Password:")]
        public string Userpassword { get; set; }
        public string Role { get; set; }
    }
}
