using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReadNovels.WebApi.Controllers
{
    using ReadNovels.Model;
    using ReadNovels.IService;

    [RoutePrefix("power")]
    public class PowerController : ApiController
    {
        private const int PageCount = 3;
        IPowerService ipowerservice = null;
        /// <summary>
        /// 构造函数注入点
        /// </summary>
        /// <param name="powerservice"></param>
        public PowerController(IPowerService powerservice)
        {
            ipowerservice = powerservice;
        }

        [HttpPost]
        [Route("addPowers")]
        public int AddPowers(Power p)
        {
            return ipowerservice.AddPowers(p);
        }

        [HttpGet]
        [Route("delete")]
        public int DeletePower(int Id)
        {
            return ipowerservice.DeletePower(Id);
        }

        [HttpGet]
        [Route("getPower")]
        public List<Power> GetPower()
        {
            var result = ipowerservice.GetPowers();
            return result;
        }

        [HttpGet]
        [Route("getPowers")]
        public Page GetPowers(string PowerName,int Page=1)
        {
            var result = ipowerservice.GetPowers();
            Page p = new Page();
            if (!string.IsNullOrWhiteSpace(PowerName))
            {
                result = result.Where(r => r.PowerName.Contains(PowerName)).ToList();
            }
            p.Currentpage = Page;
            p.Totalpage = (result.Count / PageCount) + (result.Count % PageCount == 0 ? 0 : 1);
            p.Data = result.Skip((Page - 1) * PageCount).Take(PageCount);
            return p;
        }

        [HttpGet]
        [Route("GetPowersById")]
        public List<Power> GetPowersById(int Id)
        {
            return ipowerservice.GetPowersById(Id);
        }

        [HttpPost]
        [Route("UpdatePowers")]
        public int UpdatePowers(Power p)
        {
            return ipowerservice.UpdatePowers(p);
        }
    }
}
