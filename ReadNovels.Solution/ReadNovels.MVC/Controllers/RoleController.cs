using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ReadNovels.MVC.Controllers
{
    using ReadNovels.MVC.Fatier;
    public class RoleController : Controller
    {        
        [LoginFatier]
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }
        [LoginFatier]
        public ActionResult AddRole()
        {
            return View();
        }
        [LoginFatier]
        public ActionResult UpdateRole()
        {
            return View();
        }
    }
}