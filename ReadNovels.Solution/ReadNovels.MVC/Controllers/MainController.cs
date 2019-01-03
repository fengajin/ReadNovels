using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadNovels.MVC.Controllers
{
    using Model;
    using Newtonsoft.Json;
    using ReadNovels.MVC.Fatier;
    using Utility;

    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }
        public string GetPowerList(string id)
        {
            Managers m = RedisHelper.Get<Managers>(id);
            string powers = JsonConvert.SerializeObject(m.PowerList);
            return powers;
        }
    }
}