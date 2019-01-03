using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadNovels.MVC.Fatier
{
    public class LoginFatier: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //filterContext.HttpContext.Session["URL"] = filterContext.HttpContext.Request.RawUrl;
            if (filterContext.HttpContext.Session["MenagerName"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/Managerss/Login");
            }
            //base.OnAuthorization(filterContext);
        }
    }
}