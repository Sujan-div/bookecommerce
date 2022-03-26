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
    public class cartController : Controller
    {
        public IActionResult Index(int id)
        {
           Cart cart = new Cart();
            using (IDbConnection db = new SqlConnection(Dapper.Connection))
            {
                IDbConnection connection = new SqlConnection(Dapper.Connection);
                connection.Open();
                cart = db.Query<Cart>("select book.bookid, book.bookname, book.bookauthor, book.bookprice, book.bookimage from book inner join book on cart.bookid = book.bookid WHERE Bookid =" + id, new { id }).SingleOrDefault();//only takes single value or default value
            }
            return View(cart);
        }

        public IActionResult addtocart(int id)
        {
            using (IDbConnection db = new SqlConnection(Dapper.Connection))
            {
                string sqlQuery = "Insert Into cart ( bookid, userid) Values( @bookid, @userid)";

                var rowsAffected = db.Execute(sqlQuery, new { bookid = id, userid = HttpContext.Session.GetString("userid") });
          
            }
            return RedirectToAction("Index");
        }
    }
}
