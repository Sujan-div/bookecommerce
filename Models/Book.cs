using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace bookecommercewebsite.Models
{
    public partial class Book
    {
       
        public int Bookid { get; set; }

        [Required]
        [Display(Name = "Book Name:")]
        public string Bookname { get; set; }

        [Required]
        [Display(Name = "Book Author:")]
        public string Bookauthor { get; set; }

        [Required]
        [Display(Name = "Book Price:")]
        public string Bookprice { get; set; }

        [Required]
        [Display(Name = "Book Category:")]
        public int? Bookcatid { get; set; }

        [Required]
        [Display(Name = "Book Category:")]
        public string Bookcatname { get; set; }

        [Required]
        [Display(Name = "Book Image:")]
        public string Bookimage { get; set; }

        public int Bookquantity { get; set; }
    }
}
