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
    public class bookcatController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("role") == "admin")
            {
                IDbConnection connection = new SqlConnection(Dapper.Connection);
                connection.Open();
                var data = connection.Query<Bookcat>("select * from bookcat");
                return View(data);
            }
            else
            {
                return RedirectToAction(controllerName: "Login", actionName: "Index");
            }
        }
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("role") == "admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction(controllerName: "Login", actionName: "Index");
            }
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Bookcat bookcat)
        {
            if (HttpContext.Session.GetString("role") == "admin")
            {
                try
                {
                    using (IDbConnection db = new SqlConnection(Dapper.Connection))
                    {
                        db.Open();
                        string sqlQuery = "Insert Into bookcat ( bookcatname ) Values( @bookcatname)";

                        var rowsAffected = db.Execute(sqlQuery, new { Bookcatname = bookcat.Bookcatname });
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

        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("role") == "admin")
            {
                Bookcat bookcat = new Bookcat();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {

                    bookcat = db.Query<Bookcat>("Select * From bookcat WHERE bookcatid =" + id, new { id }).SingleOrDefault();//only takes single value or default value
                }
                return View(bookcat);
            }
            else
            {
                return RedirectToAction(controllerName: "Login", actionName: "Index");
            }
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Bookcat bookcat)
        {
            if (HttpContext.Session.GetString("role") == "admin")
            {
                try
                {
                    using (IDbConnection db = new SqlConnection(Dapper.Connection))
                    {
                        string sqlQuery = "UPDATE bookcat set bookcatname='" + bookcat.Bookcatname +

                     "' WHERE bookcatid=" + bookcat.Bookcatid;

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

        public ActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("role") == "admin")
            {
                Bookcat book = new Bookcat();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {

                    book = db.Query<Bookcat>("Select * From bookcat WHERE bookcatid =" + id, new { id }).SingleOrDefault();//only takes single value or default value
                }
                return View(book);
            }
            else
            {
                return RedirectToAction(controllerName: "Login", actionName: "Index");
            }
        }

        // POST: 
        [HttpPost]
        public ActionResult Details(int id, Bookcat bookcat)
        {
            if (HttpContext.Session.GetString("role") == "admin")
            {
                try
                {
                    using (IDbConnection db = new SqlConnection(Dapper.Connection))
                    {
                        string sqlQuery = "Select bookcat from bookcatname='" + bookcat.Bookcatname +

                             "' WHERE bookcatid=" + id;

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

        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("role") == "admin")
            {
                Bookcat bookcat = new Bookcat();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    bookcat = db.Query<Bookcat>("Select * From bookcat WHERE bookcatid =" + id, new { id }).SingleOrDefault();
                }
                return View(bookcat);
            }
            else
            {
                return RedirectToAction(controllerName: "Login", actionName: "Index");
            }
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Bookcat bookcat)
        {
            if (HttpContext.Session.GetString("role") == "admin")
            {
                try
                {
                    using (IDbConnection db = new SqlConnection(Dapper.Connection))
                    {
                        string sqlQuery = "Delete From bookcat WHERE bookcatid = " + id;

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
