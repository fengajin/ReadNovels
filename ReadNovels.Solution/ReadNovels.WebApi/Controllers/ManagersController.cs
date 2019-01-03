using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ReadNovels.Model;
using ReadNovels.Service;
using ReadNovels.IService;


/// <summary>
/// 后台显示控制器
/// </summary>
namespace ReadNovels.WebApi.Controllers
{
    /// <summary>
    /// 后台显示控制器
    /// </summary>
    [RoutePrefix("managers")]
    public class ManagersController : ApiController
    {
        //页面尺寸
        private const int PageCount = 3;

        public IManagerService _managerservice = null;
        /// <summary>
        /// 构造函数注入点
        /// </summary>
        /// <param name="managerService"></param>
        public ManagersController(IManagerService managerService)
        {
            _managerservice = managerService;
        }

        /// <summary>
        /// 后台显示方法
        /// </summary>
        /// <param name="ManagersName"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("getManagers")]
        [HttpGet]
        public Page GetManagers(string ManagersName,int Page=1)
        {
            var result = _managerservice.GetManagers();
            Page page = new Page();
            if (!string.IsNullOrWhiteSpace(ManagersName))
            {
                result = result.Where(r => r.ManagersName.Contains(ManagersName)).ToList();
            }
            page.Currentpage = Page;
            page.Totalpage = (result.Count / PageCount) + (result.Count % PageCount == 0 ? 0 : 1);
            page.Data = result.Skip((Page - 1) * PageCount).Take(PageCount);
            return page;
        }

        /// <summary>
        /// 后台登录
        /// </summary>
        /// <param name="ManagersName"></param>
        /// <param name="ManagersPsw"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("houTaiLogin")]
        public Managers Login(string ManagersName, string ManagersPsw)
        {
            return _managerservice.Login(ManagersName,ManagersPsw);
        }

        [HttpPost]
        [Route("addManagers")]
        public int AddManagers(Managers managers)
        {
            var result = _managerservice.AddManagers(managers);
            return result;
        }

        [HttpGet]
        [Route("deleteManagers")]
        public int DeleteManagers(int Id)
        {
            var result = _managerservice.DeleteManagers(Id);
            return result;
        }

        [HttpPost]
        [Route("updateManagers")]
        public int UpdateManagers(Managers managers)
        {
            var result = _managerservice.UpdateManagers(managers);
            return result;
        }

        [HttpGet]
        [Route("getManagersById")]
        public List<Managers> GetManagersById(int Id)
        {
            var result = _managerservice.GetManagersById(Id);
            return result;
        }

    }
}
