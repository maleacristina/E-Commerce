 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    [AllowAnonymous]
    public class BooksController : Controller
    {
        private ApplicationDbContext _context;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Books
        //public ActionResult Index(string searchBy, string search)
        //{
        //    if (searchBy == "Name")
        //    {
        //        return View(_context.Books.Where(x => x.Name.StartsWith(search) || search == null).ToList());
        //    }
        //    else
        //    {
        //        return View(_context.Books.Where(x => x.Author.StartsWith(search) || search == null).ToList());
        //    }


        //    //if (price >= 10 || price <= 20)
        //    //    return View(_context.Books.Where(x => x.Price == price).ToList());
        //    //return View(_context.Books.ToList());
        //}

        // GET: Books
        public ActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        [HttpPost]
        public ActionResult Index(string searchTerm)
        {
            _context = new ApplicationDbContext();
            List<Book> books;
            if (string.IsNullOrEmpty(searchTerm))
            {
                books = _context.Books.ToList();
            }
            else
            {
                books = _context.Books
                    .Where(s => s.Name.StartsWith(searchTerm)).ToList();
            }
            return View(books);
        }

        public ActionResult getbooks(string term)
        {
            return Json(_context.Books.Where(c => c.Name.StartsWith(term)).Select(a => new { label = a.Name, id = a.BookId }), JsonRequestBehavior.AllowGet);
            //return Json(db.Countries.Where(c => c.Name.StartsWith(term)).Select(a => new { label = a.Name }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Sort(string sortOrder)
        {
            ViewBag.PriceParam = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            var books = _context.Books.Select(x => x);

            switch (sortOrder)
            {
                case "price_desc":
                    books = books.OrderByDescending(b => b.Price);
                    break;
                default:
                    books = books.OrderBy(b => b.Price);
                    break;
            }

            return PartialView(books.ToList());
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
            var categoryModel = _context.Categories.Include("Books").SingleOrDefault(a => a.Nume == category);
            return View(categoryModel);
        }

        // GET: /Books/CategoryMenu
        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var categories = _context.Categories.ToList();
            //if (User.IsInRole("Admin"))
        

            return PartialView(categories);
        }

  /*      [ChildActionOnly]*/
        //public ActionResult PriceMenu()
        //{
        //    var prices = _context.Books.Select(x => x.Price).ToList();

        //    if (Enumerable.Range(10,50).Contains(prices))
        //        return PartialView(_context.Books.Where(x => x.Price == 10 || x.Price == 50).ToList());
        //    else if (price >= 50 || price <= 100)
        //        return PartialView(_context.Books.Where(x => x.Price == price).ToList());
        //    else if (price >= 100 || price <= 200)
        //        return PartialView(_context.Books.Where(x => x.Price == price).ToList());
        //    return null;
        //}
    }

    
}