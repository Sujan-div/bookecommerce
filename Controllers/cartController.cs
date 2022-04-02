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
        public IActionResult Index()
        {
            
            
            IDbConnection connection = new SqlConnection(Dapper.Connection);
            connection.Open();
            var data = connection.Query<Cart>("select cart.userid, cart.bookid, book.bookname, book.bookauthor, book.bookprice, book.bookimage from cart join book on cart.bookid = book.bookid where userid= "+  HttpContext.Session.GetString("userid"));
            return View(data);
        }

     
        public IActionResult addtocart(int id)
        {
            using (IDbConnection db = new SqlConnection(Dapper.Connection))
            {
                string sqlQuery = "Insert Into cart ( bookid, userid, quantity) Values( @bookid, @userid, @quantity)";

                var rowsAffected = db.Execute(sqlQuery, new { bookid = id, userid = HttpContext.Session.GetString("userid"), quantity = 1 });
          
            }
            return RedirectToAction("Index");
        }

        public IActionResult Order(int id)
        {
            using (IDbConnection db = new SqlConnection(Dapper.Connection))
            {
                string sqlQuery = "Insert Into cart ( bookid, userid, quantity) Values( @bookid, @userid, @quantity)";

                var rowsAffected = db.Execute(sqlQuery, new { bookid = id, userid = HttpContext.Session.GetString("userid"), quantity = 1 });

            }
            return RedirectToAction("Index");
        }
    }
}
