using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cart.Models.carts
{
    public class operation
    {
        public static Models.carts.CartAll getAllCart()
        {
            if(HttpContext.Current != null)
            {
                if(System.Web.HttpContext.Current.Session["cart"] == null)
                {
                    CartAll order = new CartAll();
                    System.Web.HttpContext.Current.Session["cart"] = order;

                }
                return (CartAll)System.Web.HttpContext.Current.Session["cart"];
            }
            
            else
            {
                throw new InvalidOperationException("請求為空");
                
            }
        }
        
    }
}