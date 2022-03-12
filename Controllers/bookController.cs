using bookecommercewebsite.Models;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace bookecommercewebsite.Controllers
{
    public class bookController : Controller
    {
       
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly bookecommerceContext adb;

        public bookController(IWebHostEnvironment hostEnvironment)
        {
           
            webHostEnvironment = hostEnvironment;
            
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("role") == "admin")
            {
                IDbConnection connection = new SqlConnection(Dapper.Connection);
                connection.Open();
                var data = connection.Query<Book>("select book.bookid, book.bookname, book.bookauthor, book.bookprice, book.bookimage, bookcat.bookcatname from book inner join bookcat on book.bookcatid = bookcat.bookcatid");
                return View(data);
            }
            else
            {
                return RedirectToAction(controllerName: "Login", actionName: "Index");
            }

        }
        // GET: Customer/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("role") == "admin")
            {
                var book = new Bookcat();
                IDbConnection connection = new SqlConnection(Dapper.Connection);
                var bookdata = connection.Query<Bookcat>("select * from bookcat").ToList();
                ViewBag.TotalSubs = new SelectList(bookdata, "Bookcatid", "Bookcatname");
                return View();
            }
            else
            {
                return RedirectToAction(controllerName: "Login", actionName: "Index");
            }
        }

        // POST: Customer/Create
        [HttpPost]
        public async Task<IActionResult> Create(Book book, IFormFile ifile)
        {

            if (HttpContext.Session.GetString("role") == "admin")
            {

                try
                {


                    using (IDbConnection db = new SqlConnection(Dapper.Connection))
                    {
                        string imgext = Path.GetExtension(ifile.FileName);
                        if (imgext == ".jpg" || imgext == ".gif")
                        {
                            var saveimg = Path.Combine(webHostEnvironment.WebRootPath, ("Images"), ifile.FileName);
                            var stream = new FileStream(saveimg, FileMode.Create);
                            await ifile.CopyToAsync(stream);

                            book.Bookimage = ifile.FileName;
                            


                            var bookdata = db.Query<Bookcat>("select * from bookcat").ToList();
                            ViewBag.TotalSubs = new SelectList(bookdata, "Bookcatid", "Bookcatname");
                            db.Open();
                            string sqlQuery = "Insert Into book ( Bookname, Bookauthor, Bookprice, Bookimage, Bookcatid) Values( @Bookname, @Bookauthor, @Bookprice, @Bookimage, @Bookcatid)";

                            var rowsAffected = db.Execute(sqlQuery, new { Bookname = book.Bookname, Bookauthor = book.Bookauthor, Bookprice = book.Bookprice, Bookimage = book.Bookimage, Bookcatid = book.Bookcatid });
                        }
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
                Book book = new Book();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    var bookdata = db.Query<Bookcat>("select * from bookcat").ToList();
                    ViewBag.TotalSubs = new SelectList(bookdata, "Bookcatid", "Bookcatname");
                    book = db.Query<Book>("select book.bookid, book.bookname, book.bookauthor, book.bookprice, bookcat.bookcatname from book inner join bookcat on book.bookcatid = bookcat.bookcatid WHERE Bookid =" + id, new { id }).SingleOrDefault();//only takes single value or default value
                }
                return View(book);
            }
            else
            {
                return RedirectToAction(controllerName: "Login", actionName: "Index");
            }
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Book books, IFormFile ifile)
        {
            if (HttpContext.Session.GetString("role") == "admin")
            {
                try
                {
                    using (IDbConnection db = new SqlConnection(Dapper.Connection))
                    {
                        string imgext = Path.GetExtension(ifile.FileName);
                        if (imgext == ".jpg" || imgext == ".gif")
                        {
                            var saveimg = Path.Combine(webHostEnvironment.WebRootPath, ("Images"), ifile.FileName);
                            var stream = new FileStream(saveimg, FileMode.Create);
                            await ifile.CopyToAsync(stream);

                            books.Bookimage = ifile.FileName;

                            var bookdata = db.Query<Bookcat>("select * from bookcat").ToList();
                            ViewBag.TotalSubs = new SelectList(bookdata, "Bookcatid", "Bookcatname");
                            string sqlQuery = "UPDATE book set Bookname='" + books.Bookname +
                     "',Bookauthor='" + books.Bookauthor +
                     "',Bookprice='" + books.Bookprice +
                     "',Bookimage='" + books.Bookimage +
                     "',Bookcatid='" + books.Bookcatid +
                     "' WHERE Bookid=" + books.Bookid;

                            int rowsAffected = db.Execute(sqlQuery);
                        }
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
                Book book = new Book();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {

                    book = db.Query<Book>("select book.bookid, book.bookname, book.bookauthor, book.bookprice, bookcat.bookcatname from book inner join bookcat on book.bookcatid = bookcat.bookcatid WHERE Bookid =" + id, new { id }).SingleOrDefault();//only takes single value or default value
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
        public ActionResult Details(int id, Book book)
        {
            if (HttpContext.Session.GetString("role") == "admin")
            {
                try
                {
                    using (IDbConnection db = new SqlConnection(Dapper.Connection))
                    {
                        string sqlQuery = "Select book from Bookname='" + book.Bookname +
                             "',Bookauthor='" + book.Bookauthor +
                             "',Bookprice='" + book.Bookprice +
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
        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("role") == "admin")
            {
                Book book = new Book();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    var bookdata = db.Query<Bookcat>("select * from bookcat").ToList();
                    ViewBag.TotalSubs = new SelectList(bookdata, "Bookcatid", "Bookcatname");
                    book = db.Query<Book>("select book.bookid, book.bookname, book.bookauthor, book.bookprice, bookcat.bookcatname from book inner join bookcat on book.bookcatid = bookcat.bookcatid WHERE Bookid =" + id, new { id }).SingleOrDefault();
                }
                return View(book);
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
                        var bookdata = db.Query<Bookcat>("select * from bookcat").ToList();
                        ViewBag.TotalSubs = new SelectList(bookdata, "Bookcatid", "Bookcatname");
                        string sqlQuery = "Delete From book WHERE Bookid = " + id;

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
