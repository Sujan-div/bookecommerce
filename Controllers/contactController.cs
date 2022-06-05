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
    public class contactController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("role") == "admin")
            {
                IDbConnection connection = new SqlConnection(Dapper.Connection);
                connection.Open();
                var data = connection.Query<Contact>("select contact.contactid, contact.contactname, contact.contactemail, contact.contactsubject, contact.contactnotes from contact");
                return View(data);
            }
            else
            {
                return RedirectToAction(controllerName: "Login", actionName: "Index");
            }
        }


        public ActionResult Create()
        {
            
                return View();
           
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Contact contact)
        {
          
                try
                {
                    using (IDbConnection db = new SqlConnection(Dapper.Connection))
                    {
                        db.Open();
                        string sqlQuery = "Insert Into contact ( contactname, contactemail, contactsubject, contactnotes ) Values(  @contactname, @contactemail, @contactsubject, @contactnotes)";

                        var rowsAffected = db.Execute(sqlQuery, new { contactname = contact.Contactname, contactemail = contact.Contactemail, contactsubject = contact.Contactsubject, contactnotes = contact.Contactnotes });
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View();
                }
            
        }


        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("role") == "admin")
            {
                Contact contact = new Contact();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    contact = db.Query<Contact>("Select * From contact WHERE contactid =" + id, new { id }).SingleOrDefault();
                }
                return View(contact);
            }
            else
            {
                return RedirectToAction(controllerName: "Login", actionName: "Index");
            }
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Contact contact)
        {
            if (HttpContext.Session.GetString("role") == "admin")
            {
                try
                {
                    using (IDbConnection db = new SqlConnection(Dapper.Connection))
                    {
                        string sqlQuery = "Delete From contact WHERE contactid = " + id;

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


    }
}
