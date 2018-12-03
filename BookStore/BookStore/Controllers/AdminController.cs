using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.ViewModels;

namespace BookStore.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext _context;
        public AdminController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Admin
        public ActionResult Index()
        {

            return View(_context.Books.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            var categories = _context.Categories.ToList();

            var viewModel = new NewBook()
            {
                Book = new Book(),
                Categories = categories
            };
            return View("Create", viewModel);
        }

        //GET: /Books/AddToCart/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Books.Add(book);
            //    _context.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //if (!ModelState.IsValid)
            //{
            //     book.Categories = _context.Categories.ToList();

            //    return View("Create", book);
            //}

            var newBook = new Book
            {
                Name = book.Name,
                Author = book.Author,
                CategoryId = book.CategoryId,
                PublishingHouse = book.PublishingHouse,
                Description = book.Description,
                AvailableQuantity = book.AvailableQuantity,
                ImagePath = book.ImagePath,
                Price = book.Price
            };

            _context.Books.Add(newBook);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var categories = _context.Categories.ToList();


            NewBook book = new NewBook
            {
                Book = _context.Books.Single(c => c.BookId == id),
                Categories = categories
            };
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                //var newBook = new Book
                //{
                //    Name = book.Name,
                //    Author = book.Author,
                //    CategoryId = book.CategoryId,
                //    PublishingHouse = book.PublishingHouse,
                //    Description = book.Description,
                //    AvailableQuantity = book.AvailableQuantity,
                //    ImagePath = book.ImagePath,
                //    Price = book.Price
                //};
                var currentBook = _context.Books.Single(c => c.BookId == book.BookId);
                currentBook.Name = book.Name;
                currentBook.Author = book.Author;
                currentBook.CategoryId = book.CategoryId;
                currentBook.PublishingHouse = book.PublishingHouse;
                currentBook.Description = book.Description;
                currentBook.AvailableQuantity = book.AvailableQuantity;
                currentBook.ImagePath = book.ImagePath;
                currentBook.Price = book.Price;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("Edit");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            //Get the cart
            var bookItem = _context.Books
                .Single(c => c.BookId == id);

    

           _context.Books.Remove(bookItem);

            _context.SaveChanges();
              
                //Save changes
            
            return RedirectToAction("Index");
        }


    }
}