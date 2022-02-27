using System;
using System.Collections.Generic;

#nullable disable

namespace bookecommercewebsite.Models
{
    public partial class Book
    {
        public int Bookid { get; set; }
        public string Bookname { get; set; }
        public string Bookauthor { get; set; }
        public string Bookprice { get; set; }
        public int? Bookcatid { get; set; }
        public byte[] Bookimage { get; set; }
    }
}
