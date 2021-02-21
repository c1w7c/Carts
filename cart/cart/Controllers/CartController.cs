using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cart.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult GetCart()
        {
            return PartialView("CartPartial");
        }
        public ActionResult AddToCart(int id)
        {
            Models.carts.CartAll currentCart = Models.carts.operation.getAllCart();
            currentCart.AddToProduct(id);
            return PartialView("CartPartial");
        }
    }
}