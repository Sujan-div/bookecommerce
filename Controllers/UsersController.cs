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
    public class UsersController : Controller
    {
        public IActionResult Index()
        {

            IDbConnection connection = new SqlConnection(Dapper.Connection);
            connection.Open();
            var data = connection.Query<Book>("select book.bookid, book.bookname, book.bookauthor, book.bookprice, book.bookimage, bookcat.bookcatname from book inner join bookcat on book.bookcatid = bookcat.bookcatid");
            return View(data);


        }
        public ActionResult Details(int id)
        {
            //if (HttpContext.Session.GetString("role") == "customer")
            //{
                Book book = new Book();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {

                    book = db.Query<Book>("select book.bookid, book.bookname, book.bookauthor, book.bookprice, book.bookimage, bookcat.bookcatname from book inner join bookcat on book.bookcatid = bookcat.bookcatid WHERE Bookid =" + id, new { id }).SingleOrDefault();//only takes single value or default value
                }
                return View(book);
            //}
            //else
            //{
            //    return RedirectToAction(controllerName: "Login", actionName: "Index");
            //}
        }

        // POST: 
        [HttpPost]
        public ActionResult Details(int id, Book book)
        {
            if (HttpContext.Session.GetString("role") == "customer")
            {
                try
                {
                    using (IDbConnection db = new SqlConnection(Dapper.Connection))
                    {
                        string sqlQuery = "Select book from Bookname='" + book.Bookname +
                             "',Bookauthor='" + book.Bookauthor +
                             "',Bookprice='" + book.Bookprice +
                              "',Bookimage='" + book.Bookimage +
                              "',Bookcatname='" + book.Bookcatname +
                             "' WHERE Bookid=" + id;

                        int rowsAffected = db.Execute(sqlQuery);
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction(controllerName: "Login", actionName: "Index");
            }
        }
      
        

        public IActionResult addtocart(int id, Book book)
        {
            if (HttpContext.Session.GetString("role") == "customer")
            {


                using (IDbConnection db = new SqlConnection(Dapper.Connection))
            {
                try
                {
                    string sqlQuery = "Insert Into cart ( bookid, userid, quantity, status) Values( @bookid, @userid, @quantity, @status)";

                    var rowsAffected = db.Execute(sqlQuery, new { bookid = id, userid = HttpContext.Session.GetString("userid"), quantity = 1, status = 0 });


                    //var sqlQuery1 = "select status from cart where userid = @userid";
                    //var rowsAffected1 = db.Execute(sqlQuery1, new { userid = HttpContext.Session.GetString("userid") });
                    //var data = db.ExecuteReader(sqlQuery1);
                    //data.Read();
                    //var statusdata = data["status"].ToString();
                    //HttpContext.Session.SetString("status", statusdata);







                }
                catch (Exception ee)
                {
                    ViewBag.error = "The Book is already in the cart";
                   
                }


               
            }
            return RedirectToAction(controllerName: "cart", actionName: "Index");

            }
            else
            {
                return RedirectToAction(controllerName: "Login", actionName: "Index");
            }

        }

      

    }
}
