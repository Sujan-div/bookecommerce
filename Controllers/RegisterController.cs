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
    public class RegisterController : Controller
    {
        // GET: Customer/Create
        public ActionResult Create()
        {

            return View();

        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(User user)
        {



            try
            {
                string role = "customer";
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    db.Open();
                    string sqlQuery = "Insert Into [user] ( username, useraddress, useremail, usercontact, userpassword, role) Values( @username, @useraddress, @useremail, @usercontact, @userpassword, @role)";

                    var rowsAffected = db.Execute(sqlQuery, new { username = user.Username, useraddress = user.Useraddress, useremail = user.Useremail, usercontact = user.Usercontact, userpassword = user.Userpassword, role });
                }

                return RedirectToAction(controllerName: "Login", actionName: "Index");
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("role") == "admin" || HttpContext.Session.GetString("role") == "customer")
            {
            User user = new User();
            string sql = "select * from [user] where userid=@userid";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@userid", HttpContext.Session.GetString("userid"));


            var data = Class1.RunQuery<User>(sql, parameters).SingleOrDefault();
            return View(data);

            }
            else
            {
                return RedirectToAction(controllerName: "Login", actionName: "Index");
            }
        }
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {

            if (HttpContext.Session.GetString("role") == "admin" || HttpContext.Session.GetString("role") == "customer")
            {
                string sqlQuery = "UPDATE [user] set username=@username, useraddress=@useraddress, useremail=@useremail, usercontact=@usercontact, userpassword=@userpassword WHERE userid=@userid";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@username", user.Username);
            parameters.Add("@useraddress", user.Useraddress);
            parameters.Add("@useremail", user.Useremail);
            parameters.Add("@usercontact", user.Usercontact);
            parameters.Add("@userpassword", user.Userpassword);
            parameters.Add("@userid",HttpContext.Session.GetString("userid"));

            var data = Class1.RunQuery<User>(sqlQuery, parameters);


            return RedirectToAction(controllerName: "Users", actionName: "Index");

            }
            else
            {
                return RedirectToAction(controllerName: "Login", actionName: "Index");
            }

        }


    }
}
