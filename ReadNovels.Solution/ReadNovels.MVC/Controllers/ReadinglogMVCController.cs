using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadNovels.MVC.Controllers
{
    using ReadNovels.MVC.Fatier;
    public class ReadinglogMVCController : Controller
    {
        [LoginFatier]
        // GET: ReadinglogMVC
        public ActionResult Index()
        {
            return View();
        }
    }
}