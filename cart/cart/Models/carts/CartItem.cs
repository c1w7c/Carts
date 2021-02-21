using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cart.Models.carts
{
    public class CartItem
    {
        public int Id { set; get; }
        public string Name { set; get; }

        public decimal Price { set; get; }
        public int Quantity { set; get; }
        public decimal Amount
        {
            get
            {
                return this.Price * this.Quantity;
            }
        }
    }
}