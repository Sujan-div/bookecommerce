namespace bookecommercewebsite.Models
{
    public class Cart
    {
        
        public int Bookid { get; set; }

        public int Userid { get; set; }

        public int Cartid { get; set; }

        public string Bookname { get; set; }

        public string Bookauthor { get; set; }

        public string Bookprice { get; set; }

        public string Bookimage { get; set; }
    }
}