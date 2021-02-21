using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cart.Models.carts
{
    public class CartAll : IEnumerable<CartItem>
    {
        public List<CartItem> cart;
        public CartAll()
        {
            cart = new List<CartItem>();
        }

        public decimal totalAmount()
        {
            decimal total = 0.0m;
            foreach (CartItem c in cart)
            {
                total += c.Amount;
            }
            return total;
        }
        public void AddToProduct(int id)
        {
            var findItem = this.cart.Where(s => s.Id == id).Select(s => s).FirstOrDefault();
            if (findItem == default(Models.carts.CartItem))
            {
                using (Models.cartEntities _db = new Models.cartEntities())
                {
                    var product = (from s in _db.Products where s.Id == id select s).FirstOrDefault();
                    if (product != default(Models.Product))
                    {
                        this.AddToProduct(product);
                    }
                }
            }
            else
            {
                findItem.Quantity++;
            }
        }

        //將產品加入倒購物車內
        private void AddToProduct(Models.Product product)
        {
            Models.carts.CartItem cartItem = new Models.carts.CartItem()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = 1
            };
            this.cart.Add(cartItem);

        }

        public IEnumerator<CartItem> GetEnumerator()
        {
            return ((IEnumerable<CartItem>)cart).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<CartItem>)cart).GetEnumerator();
        }
    }
}