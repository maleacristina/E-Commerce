using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        ApplicationDbContext _context;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Books
        public ActionResult Index()
        {
            //var book = new Book()
            //{
            //    Name = "Baltagul"
            //};
            return View(_context.Books.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            Book book = _context.Books.SingleOrDefault((c => c.BookId == id));
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        public ActionResult Browse(string category)
        {
            var categoryModel = _context.Categories.Include("Books").Single(a => a.Nume == category);
            return View(categoryModel);
        }

        // GET: /Books/CategoryMenu
        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var categories = _context.Categories.ToList();
            return PartialView(categories);
        }
    }
}