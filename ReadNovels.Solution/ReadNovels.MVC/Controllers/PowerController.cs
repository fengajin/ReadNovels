using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadNovels.MVC.Controllers
{
    using ReadNovels.MVC.Fatier;
    public class PowerController : Controller
    {
        [LoginFatier]
        // GET: Power
        public ActionResult Index()
        {

            return View();
        }
        [LoginFatier]
        public ActionResult AddPower()
        {
            return View();
        }
        [LoginFatier]
        public ActionResult Update()
        {
            return View();
        }
    }
}