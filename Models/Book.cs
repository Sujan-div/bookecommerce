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

        [Display(Name = "Book Name:")]
        public string Bookname { get; set; }

        [Display(Name = "Book Author:")]
        public string Bookauthor { get; set; }

        [Display(Name = "Book Price:")]
        public string Bookprice { get; set; }

        [Display(Name = "Book Category:")]
        public int? Bookcatid { get; set; }

        [Display(Name = "Book Category:")]
        public string Bookcatname { get; set; }

        [Display(Name = "Book Image:")]
        public string Bookimage { get; set; }
    }
}
