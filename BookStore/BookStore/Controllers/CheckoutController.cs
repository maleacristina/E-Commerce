using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class CheckoutController : Controller
    {
        ApplicationDbContext _context;

        public CheckoutController()
        {
            _context = new ApplicationDbContext();
        }

        //const string PromoCode = "FREE";
        // GET: Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        //POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();


            TryUpdateModel(order);

            order.Username = User.Identity.Name;
            order.OrderDate = DateTime.Now;

            //Save order
            _context.Orders.Add(order);
            _context.SaveChanges();

            //Process the order
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.CreateOrder(order);

            return RedirectToAction("Complete",
                new { id = order.Id });
            //}
            //catch
            //{
            //    return View(order);
            //}
        }

        //GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            //Validate customer owns this order
            bool isValid = _context.Orders.Any(
                o => o.Id == id &&
                     o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

        //public ActionResult Index()
        //{
        //    throw new NotImplementedException();
        //}
    }
}