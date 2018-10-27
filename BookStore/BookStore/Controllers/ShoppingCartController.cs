using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.ViewModels;

namespace BookStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext _context;
        public ShoppingCartController()
        {
            _context = new ApplicationDbContext();
        }


        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            //Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            return View(viewModel);
        }

        //GET: /Cakes/AddToCart/5
        [Authorize]
        public ActionResult AddToCart(int id)
        {
            var addedBook = _context.Books.
                Single(c => c.BookId == id);

            //Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedBook);

            return RedirectToAction("Index");
        }

        //AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            //Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            //Get the name of the cake to display confirmation
            string bookName = _context.Carts.
                Single(c => c.RecordId == id).
                Book.Name;

            //Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            //Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(bookName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }

        //GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }

}