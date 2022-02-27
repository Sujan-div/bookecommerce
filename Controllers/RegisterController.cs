using bookecommercewebsite.Models;
using Dapper;
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
    }
}
