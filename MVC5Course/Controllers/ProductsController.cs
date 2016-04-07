﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    [Controller執行時間]
    [Authorize]
    public class ProductsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();
        //ProductRepository repo = new ProductRepository();

        // GET: Products
        public ActionResult Index()
        {
            //return View(db.Product.ToList());
            var data = repoProduct.All().Take(5);
            return View(data);
        }

        [HttpPost]
        public ActionResult Index(IList<BatchUpdateProducts> data)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    var product = repoProduct.Find(item.ProductId);

                    product.Price = item.Price;
                    product.Active = item.Active;
                    product.Stock = item.Stock;
                }
                repoProduct.UnitOfWork.Commit();
                return RedirectToAction("Index");

            }



            //驗證果有錯誤，驗證失敗，就要從心抓原始資料，並秀出錯誤訊息
            ViewData.Model = repoProduct.All().Take(5);
            return View();

        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repoProduct.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Product.Add(product);
                //db.SaveChanges();
                repoProduct.Add(product);
                repoProduct.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repoProduct.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        
        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        //{                         //只會繫結這邊有寫到的屬性，沒寫到的就會給定預設值，例如：bool->預設值為false
        public ActionResult Edit(int id, FormCollection form)
        {
            var product = repoProduct.Find(id);

            if (TryUpdateModel<Product>(product, new string[] { "ProductId","ProductName","Price","Active","Stock" }))
            {
                TempData["Msg"] = product.ProductName + " 更新成功!";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repoProduct.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Product product = db.Product.Find(id);
            //db.Product.Remove(product);
            //db.SaveChanges();
            Product product = repoProduct.Find(id);
            repoProduct.Delete(product);
            repoProduct.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                repoProduct.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
