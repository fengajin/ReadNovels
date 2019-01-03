using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadNovels.MVC.Controllers
{
    using ReadNovels.MVC.Fatier;
    /// <summary>
    /// 抓取小说MVC控制器
    /// </summary>
    public class CaptureMvcController : Controller
    {
        /// <summary>
        /// 抓取小说视图
        /// </summary>
        /// <returns></returns>
        [LoginFatier]
        public ActionResult Index()
        {
            return View();
        }
        
    }
}