using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cart.Controllers
{
    public class testController : Controller
    {
        // GET: test
       public System.Web.Mvc.ActionResult getCart()
        {
            var cart = Models.carts.operation.getAllCart();
            if(cart.cart.Count == 0)
            {
                cart.cart.Add(new Models.carts.CartItem()
                {
                    Id = 1,
                    Name = "測試",
                    Quantity = 3,
                    Price = 100m
                });
            }
            else
            {
                cart.cart.First().Quantity += 1;
            }

            return Content(String.Format("{0}",cart.totalAmount()));

        }

    }
}