using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadNovels.MVC.Controllers
{
    using ReadNovels.Model;
    using ReadNovels.MVC.Fatier;
    public class NovelAddedController : Controller
    {
        [LoginFatier]
        // GET: NovelAdded
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 小说上架
        /// </summary>
        /// <returns></returns>
        /// 
        [LoginFatier]
        public ActionResult NovelAdded()
        {
            return View();
        }
    }
}