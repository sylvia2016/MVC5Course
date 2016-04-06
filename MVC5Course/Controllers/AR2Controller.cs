using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class AR2Controller : BaseController
    {
        // GET: AR2
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult PartialViewTest()
        {
            return PartialView("Index");
        }
        public ActionResult FileTest(int? dl)
        {
            if (dl.HasValue)
            {
                return File(Server.MapPath("~/Content/friend14.png"), "image/png", "My picture.png");
            }
            else
            {
                return File(Server.MapPath("~/Content/friend14.png"), "image/png");
            }
        }

        public ActionResult JsonTest(int? id)
        {
            repoProduct.UnitOfWork.Context.Configuration.LazyLoadingEnabled = false;
            var product = repoProduct.Find(id.Value);

            return Json(product, JsonRequestBehavior.AllowGet);
        }
    }
}