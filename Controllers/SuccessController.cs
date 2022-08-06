using bookecommercewebsite.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace bookecommercewebsite.Controllers
{
    public class SuccessController : Controller
    {
        public IActionResult Success()
        {
            IDbConnection connection = new SqlConnection(Dapper.Connection);
            connection.Open();
            var data = connection.Query<Book>("select book.bookid, book.bookname, book.bookauthor, book.bookprice, book.bookimage, bookcat.bookcatname from book inner join bookcat on book.bookcatid = bookcat.bookcatid");
            return View(data);
        }
    }
}
