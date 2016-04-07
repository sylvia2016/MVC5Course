using System;
using System.Web.Mvc;

namespace MVC5Course.Models
{
    internal class Controller執行時間Attribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.startTime = DateTime.Now;
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.endTime = DateTime.Now;

            var timeSpan = filterContext.Controller.ViewBag.endTime
                         - filterContext.Controller.ViewBag.startTime;

            filterContext.Controller.ViewBag.timeSpan = timeSpan;

            base.OnActionExecuted(filterContext);
        }
    }
}