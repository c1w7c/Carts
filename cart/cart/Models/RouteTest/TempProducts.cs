using System.Collections.Generic;

namespace cart.Models.RouteTest
{
    public class TempProducts
    {
        public int id { set; get; }
        public string name { set; get; }
        public decimal price { set; get; }

        public static List<TempProducts> getAllProducts()
        {
            List<TempProducts> products = new List<TempProducts>();
            products.Add(new cart.Models.RouteTest.TempProducts()
            {
                id = 1,
                name = "拖把",
                price = 999

            });
            products.Add(new cart.Models.RouteTest.TempProducts()
            {
                id = 2,
                name = "掃把",
                price = 799

            });
            products.Add(new cart.Models.RouteTest.TempProducts()
            {
                id = 3,
                name = "水桶",
                price = 199

            });

            return products;
        }

    }
}
