using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class TestController : BaseController
    {
        FabricsEntities db = new FabricsEntities();

        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EDE()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EDE(EDEViewModel data)
        {
            return View(data);
        }

        public ActionResult createProduct()
        {

            var product = new Product()
            {
                ProductName = "A",
                Active = false,
                Price = 20,
                Stock = 5
            };

            db.Product.Add(product);
            db.SaveChanges();
            return View(product);
        }

        public ActionResult ReadProduct(bool? Active)
        {
            var data = db.Product.OrderByDescending(_p => _p.Price).AsQueryable();

            data = data.Where(_p => _p.ProductId > 1550);

            if (Active.HasValue)
            {
                data = data.Where(_p => _p.Active == Active);
            }

            return View(data);
        }

        public ActionResult OneProduct(int id)
        {
            var data = db.Product.Find(id);
            return View(data);
        }

        public ActionResult UpdateProduct(int id)
        {
            var one = db.Product.FirstOrDefault(_p => _p.ProductId == id);

            if (one == null)
                return HttpNotFound();

            one.Price = one.Price * 2;

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityError in ex.EntityValidationErrors)
                {
                    foreach (var err in entityError.ValidationErrors)
                    {
                        return Content(err.PropertyName + "：" + err.ErrorMessage);
                    }
                }
                throw;
            }

            return RedirectToAction("ReadProduct");
        }

        public ActionResult DeleteProduct(int id)
        {
            var one = db.Product.Find(id);          

            //因為有fk的關係，所以要先把關聯的table資料刪除Product對OrderLine是一對多
            foreach (var item in one.OrderLine.ToList())  //OrderLine為導覽屬性
            {
                db.OrderLine.Remove(item);
            }

            //db.Database.ExecuteSqlCommand(@"Delete .....@p0", id);  //一次刪除整批

            db.Product.Remove(one);
            db.SaveChanges();

            return RedirectToAction("ReadProduct");
        }

        public ActionResult ProductView()
        {
            var data = db.Database.SqlQuery<ProductViewModel>(
                @"SELECT * FROM dbo.Product WHERE Active = @p0 AND ProductName LIKE @p1", true, "%Yellow%");

            return View(data);
        }

        public ActionResult ProductSP()
        {
            var data = db.GetProducts(true, "%YELLOW%");

            return View(data);
        }
    }
}