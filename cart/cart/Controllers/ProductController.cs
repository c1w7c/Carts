using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cart.Models.RouteTest
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            List<Models.Product> result = new List<Models.Product>();
            ViewBag.ResultMessage = TempData["ResultMessage"];
            ViewBag.ResultError = TempData["ResultError"];
            using (Models.cartEntities _db = new Models.cartEntities())
            {
                
                result = (from s in _db.Products select s).ToList();
                return View(result);
            }
            
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Product item)
        {
            if (ModelState.IsValid) { 
                using(Models.cartEntities _db = new Models.cartEntities())
                {
                    _db.Products.Add(item);

                    _db.SaveChanges();

                    TempData["ResultMessage"] = String.Format("商品{0}成功建立", item.Name);

                    //redirectToAction會回傳Index的view結果回來
                    return RedirectToAction("Index");

                }
                
            }
            ViewBag.ResultMessage = "資料有誤，請檢察";

            return View();
        }

        public ActionResult Edit(int id)
        {
            //連接model 資料庫(Object service)
            using(Models.cartEntities _db = new Models.cartEntities())
            {
                var result = (from s in _db.Products where s.Id == id select s).FirstOrDefault();
                if(result != default(Models.Product))
                {
                    return View(result);
                }

                TempData["ResultMessage"] = "無此筆資料";
                return RedirectToAction("Index");
            }
            
        }
        [HttpPost]
        public ActionResult Edit(Models.Product data)
        {
            if (ModelState.IsValid)
            {
                using (Models.cartEntities _db = new Models.cartEntities())
                {
                    var result = (from s in _db.Products where s.Id == data.Id select s).First();
                    result.Name = data.Name;
                    result.Description = data.Description;
                    result.CategoryId = data.CategoryId;
                    result.Price = data.Price;
                    result.PublishDate = data.PublishDate;
                    result.Status = data.Status;
                    result.DefaultImageId = data.DefaultImageId;
                    result.Quantity = data.Quantity;

                    try
                    {
                        _db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    TempData["ResultMessage"] = String.Format("{0}修改成功", data.Name);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(data);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using(Models.cartEntities _db = new Models.cartEntities())
            {
                var result = (from s in _db.Products where s.Id == id select s).FirstOrDefault();
                if(result != null)
                {
                    _db.Products.Remove(result);
                    _db.SaveChanges();

                    TempData["ResultMessage"] = String.Format("{0}商品成功刪除", result.Name);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ResultError"] = "無此商品";
                    return RedirectToAction("Index");
                }
            }
        }
        
    }
}