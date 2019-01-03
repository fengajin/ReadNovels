using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadNovels.MVC.Controllers
{
    using ReadNovels.MVC.Fatier;
    public class NovelAuditController : Controller
    {
        [LoginFatier]
        // GET: NovelAudit
        public ActionResult Index()
        {
            return View();
        }
    }
}