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
    public class orderController : Controller
    {
        public IActionResult Index()
        {
            int Status = 1;
            IDbConnection connection = new SqlConnection(Dapper.Connection);
            connection.Open();

            //var data = connection.Query<Cart>("select cart.userid, cart.bookid, book.bookname, book.bookauthor, book.bookprice, book.bookimage from cart join book on cart.bookid = book.bookid where status = "+ Status);
            var data = connection.Query<Order>("select [order].userid, [order].bookid, book.bookname, [order].quantity, [order].status, [order].fullname, [order].email, [order].phonenumber, book.bookauthor, book.bookprice, book.bookimage from [order] join book on [order].bookid = book.bookid where status =" + Status);
            return View(data);

        }


        // GET: Order/Edit
        public ActionResult Edit(int id)
        {
            User user = new User();
            string sql = "select fullname, email, phonenumber from [order] where userid=@userid";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@userid", HttpContext.Session.GetString("userid"));


            var data = Class1.RunQuery<Order>(sql, parameters).FirstOrDefault();
            return View(data);
        }


        [HttpPost]
        public ActionResult Edit(int id, Order order)
        {


            string sqlQuery = "UPDATE [order] set fullname=@fullname, email=@email, phonenumber=@phonenumber WHERE userid=@userid";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@fullname", order.Fullname);
            parameters.Add("@email", order.Email);
            parameters.Add("@phonenumber", order.Phonenumber);
         
            parameters.Add("@userid", HttpContext.Session.GetString("userid"));

            var data = Class1.RunQuery<Order>(sqlQuery, parameters);


            return RedirectToAction(controllerName: "cart", actionName: "Index");

        }



    }
}
