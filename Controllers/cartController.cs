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

     
       

        public IActionResult Checkout(int id)
        {
            //using (IDbConnection db = new SqlConnection(Dapper.Connection))
            //{
            //    string sqlQuery = "update cart set status = @status where userid = @userid";

            //    var rowsAffected = db.Execute(sqlQuery, new { userid = HttpContext.Session.GetString("userid"), status = 1});


            //var sqlQuery1 = "select status from cart where userid = @userid and bookid = @bookid";
            //var rowsAffected1 = db.Execute(sqlQuery1, new { userid = HttpContext.Session.GetString("userid"), bookid = id });
            //var data = db.ExecuteReader(sqlQuery1);
            //data.Read();
            //var statusdata = data["status"].ToString();
            //HttpContext.Session.SetString("status", statusdata);

            //}

            //IDbConnection connection = new SqlConnection(Dapper.Connection);
            //connection.Open();

            //var sql = "select status from cart where userid =" + HttpContext.Session.GetString("userid");
            //var data = connection.ExecuteReader(sql);
            //data.Read();
            //var statusdata = data["status"].ToString();
            //HttpContext.Session.SetString("status", statusdata);




            using (IDbConnection db = new SqlConnection(Dapper.Connection))
            {
                try
                {

                    string sqlQuery = "update cart set status = @status where userid = @userid";

                    var rowsAffected = db.Execute(sqlQuery, new { userid = HttpContext.Session.GetString("userid"), status = 1 });



                    string sqlQuery2 = "Insert Into [order] ( userid, bookid, cartid, quantity, [status]) select cart.userid, cart.bookid, cart.cartid, cart.quantity, cart.status from cart where status = @status and userid = @userid";

                    var rowsAffected2 = db.Execute(sqlQuery2, new { userid = HttpContext.Session.GetString("userid"), status = 1 });


                    




                    string sqlQuery3 = "delete from cart  where status = @status and userid = @userid";

                    var rowsAffected3 = db.Execute(sqlQuery3, new { userid = HttpContext.Session.GetString("userid"), status = 1 });
                }
                catch(Exception ee)
                {

                    //again run the delete query here.

                    string sqlQuery4 = "delete from cart  where status = @status and userid = @userid";

                    var rowsAffected4 = db.Execute(sqlQuery4, new { userid = HttpContext.Session.GetString("userid"), status = 1 });
                }
                return RedirectToAction(controllerName: "order", actionName: "Edit");
            }

                return RedirectToAction("Index");
        }

        public IActionResult ViewOrder()
        {
            int Status = 1;
            IDbConnection connection = new SqlConnection(Dapper.Connection);
            connection.Open();

            //var data = connection.Query<Cart>("select cart.userid, cart.bookid, book.bookname, book.bookauthor, book.bookprice, book.bookimage from cart join book on cart.bookid = book.bookid where status = "+ Status);
            var data = connection.Query<Order>("select userid from [order]");
            return View(data);

           
        }
    }
}
