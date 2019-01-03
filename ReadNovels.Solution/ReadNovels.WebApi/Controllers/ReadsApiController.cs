using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReadNovels.WebApi.Controllers
{
    using ReadNovels.Model;
    using ReadNovels.Service;

    /// <summary>
    /// 最近阅读
    /// </summary>
    [RoutePrefix("readsApi")]
    public class ReadsApiController : ApiController
    {
        ReadsService readService = new ReadsService();

        /// <summary>
        /// 最近阅读
        /// </summary>
        /// <param name="wx_userid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getReads")]
        public List<Reads> GetReads(int wx_userid)
        {
            var readlist = readService.GetReads(wx_userid);
            return readlist;
        }

    }
}
