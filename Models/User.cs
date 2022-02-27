using System;
using System.Collections.Generic;

#nullable disable

namespace bookecommercewebsite.Models
{
    public partial class User
    {
        public int Userid { get; set; }
        public string Username { get; set; }
        public string Useraddress { get; set; }
        public string Useremail { get; set; }
        public string Usercontact { get; set; }
        public string Userpassword { get; set; }
        public string Role { get; set; }
    }
}
