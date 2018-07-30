using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace BusinessTripApplication
{
   public class ErrorHandler : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            var result = new ViewResult();
            result.ViewName = "ERROR";
            result.ViewBag.Error = "Error Occur While Processing Your Request Please Check After Some Time";
            filterContext.Result = result;
        }
    }
}
    