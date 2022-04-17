using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bookecommercewebsite.Models
{
    public class Order
    {

        public int Bookid { get; set; }

        public int Userid { get; set; }

        [Key]
        public int Cartid { get; set; }

        public string Bookname { get; set; }

        public string Bookauthor { get; set; }

        public string Bookprice { get; set; }

        public string Bookimage { get; set; }

        public int Quantity { get; set; }

        public int Status { get; set; }

        public int Orderid { get; set; }

        [Required]
        public string Fullname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int Phonenumber { get; set; }
    }
}
