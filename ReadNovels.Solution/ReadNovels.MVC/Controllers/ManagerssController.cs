using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadNovels.MVC.Controllers
{
    using Newtonsoft.Json;
    using ReadNovels.Model;
    using Utility;
    using ReadNovels.MVC.Fatier;
    public class ManagerssController: BaseController
    {
        // GET: Managerss
        [LoginFatier]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [LoginFatier]
        public ActionResult AddManagers()
        {
            return View();
        }
        [LoginFatier]
        public ActionResult UpdateManagers()
        {
            return View();
        }
        /// <summary>
        /// forms 身份认证
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public int Logins(Managers managers)
        {
            Session["Id"] = managers.Id;
            Session["MenagerName"] = managers.ManagersName;
            WriteDataToCookie(managers);
            return 1;
        }
        /// <summary>
        /// 登录首页
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult LoginIndex()
        {
            ViewBag.Id = Session["Id"];
            ViewBag.MenagerName = Session["MenagerName"];
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