using bookecommercewebsite.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace bookecommercewebsite.Controllers
{
    public class CustomerfunctionsController : Controller
    {
        public IActionResult Index()
        {
          
                IDbConnection connection = new SqlConnection(Dapper.Connection);
                connection.Open();
                var data = connection.Query<Book>("select book.bookid, book.bookname, book.bookauthor, book.bookprice, bookcat.bookcatname from book inner join bookcat on book.bookcatid = bookcat.bookcatid");
                return View(data);
           

        }
    }
}
