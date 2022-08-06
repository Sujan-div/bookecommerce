using bookecommercewebsite.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
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


    public class LoginController : Controller
    {

        public IActionResult Success()
        {
            IDbConnection connection = new SqlConnection(Dapper.Connection);
            connection.Open();
            var data = connection.Query<Book>("select book.bookid, book.bookname, book.bookauthor, book.bookprice, book.bookimage, bookcat.bookcatname from book inner join bookcat on book.bookcatid = bookcat.bookcatid");
            return View(data);
        }

        //returns view of login page
        public IActionResult Index()
        {
            return View();
        }

        //send data from login page to database and also make changes
        [HttpPost]
        //login button click action method 

        public IActionResult Login(string username, string userpassword)
        {

            try
            {

                //using service dapper from startup.cs
                IDbConnection connection = new SqlConnection(Dapper.Connection);
                //opening connection
                connection.Open();
                //sql query to select columns from table
                var sql = "select * from [user] WHERE username = @username and userpassword = @userpassword";
                //storing lib value of table in data variable
                var data = connection.ExecuteReader(sql, new { Username = username, Userpassword = userpassword });

                Console.WriteLine(data);
                //read data 
                data.Read();

                //store data in new variable from each column from data table
                var namedata = data["username"].ToString();
                var passworddata = data["userpassword"].ToString();
                var roledata = data["role"].ToString();
                var iddata = data["userid"].ToString();





                if (username != null && userpassword != null && username == namedata && userpassword == passworddata)
                {
                    HttpContext.Session.SetString("username", username);
                    HttpContext.Session.SetString("role", roledata);
                    HttpContext.Session.SetString("userid", iddata);
                    return RedirectToAction(controllerName: "Users", actionName: "Index");
                }
                else
                {
                    ViewBag.error = "Invalid Account";
                    return View("Index");
                }
            }
            catch (Exception ee)
            {
                ViewBag.error = "Invalid Account";
                return View("Index");
            }

        }

        //request data from server
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("role");
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
           
        }




    }
}
