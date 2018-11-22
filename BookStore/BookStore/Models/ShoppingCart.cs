using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Models
{
    public class ShoppingCart
    {
        ApplicationDbContext _context;

        public ShoppingCart()
        {
            _context = new ApplicationDbContext();
        }

        string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";


        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        internal static object GetCart(object httpContext)
        {
            throw new NotImplementedException();
        }

        //Metoda pentru simplificarea apelurilor la cosul de cumparaturi
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Book book)
        {
            var cartItem = _context.Carts
                .SingleOrDefault(c => c.CartId == ShoppingCartId
                && c.BookId == book.BookId);

            if (cartItem == null)
            {
                //Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    BookId = book.BookId,
                    CartId = ShoppingCartId,
                    Cantitate = 1

                };

                _context.Carts.Add(cartItem);
            }
            else
            {
                //if the item does exist in the cart,
                //then add one to the quantity
                cartItem.Cantitate++;
            }

            //Save changes
            _context.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            //Get the cart
            var cartItem = _context.Carts
                .Single(c => c.CartId == ShoppingCartId
                && c.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Cantitate > 1)
                {
                    cartItem.Cantitate--;
                    itemCount = cartItem.Cantitate;
                }
                else
                {
                    _context.Carts.Remove(cartItem);
                }
                //Save changes
                _context.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = _context.Carts
                .Where(c => c.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                _context.Carts.Remove(cartItem);
            }

            //Save changes
            _context.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return _context.Carts
                .Where(c => c.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            //Get the count og each item in the cart and sum them up
            int? count = (from cartItems in _context.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Cantitate).Sum();

            //Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            decimal? total = (from cartItems in _context.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Cantitate * cartItems.Book.Price)
                              .Sum();
            return total ?? decimal.Zero;
        }



        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    BookId = item.BookId,
                    OrderId = order.Id,
                    UnitPrice = item.Book.Price,
                    Quantity = item.Cantitate
                };

                orderTotal += (item.Cantitate * item.Book.Price);

                _context.OrderDetails.Add(orderDetail);
            }

            order.Total = orderTotal;
            _context.Orders.Add(order);
            _context.SaveChanges();
            EmptyCart();

            return order.Id;
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        public void MigrateCart(string userName)
        {
            var shoppingCart = _context.Carts.Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            _context.SaveChanges();
        }
    }
}