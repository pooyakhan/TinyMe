using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TinyMe.Filters
{
    public class TinyMeExceptionFilter: HandleErrorAttribute
    {

        public override void OnException(ExceptionContext filterContext)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            var ex = filterContext.Exception;
            string viewName = "Error500";

            if (ex is TinyMeNotFoundException)
            {
                code = HttpStatusCode.NotFound;
                viewName = "Error404";
            }
            if (ex is TinyMeConflictException)
            {
                code = HttpStatusCode.Conflict;
                viewName = "Error409";
            }

            filterContext.Result = new ViewResult()
            {
                ViewName = viewName
            };

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = (int)code;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

        }
    }
}