using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadNovels.MVC.Controllers
{
    using ReadNovels.Model;
    using ReadNovels.MVC.Fatier;
    public class NovelController : Controller
    {
        [LoginFatier]
        // GET: Novel
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 小说上架
        /// </summary>
        /// <returns></returns>
        //public ActionResult NovelAdded()
        //{
        //    return View();
        //}
    }
}