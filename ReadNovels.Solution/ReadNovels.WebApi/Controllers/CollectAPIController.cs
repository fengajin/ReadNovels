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
    using ReadNovels.Service;

    [RoutePrefix("collectAPI")]
    public class CollectAPIController : ApiController
    {

        ICollectService icollectService = null;
        /// <summary>
        /// 构造函数注入点
        /// </summary>
        /// <param name="collectService"></param>
        public CollectAPIController(ICollectService collectService)
        {
            icollectService = collectService;
        }

        /// <summary>
        /// 显示所有的收藏
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getCollect")]
        public List<Collect> GetCollect(int wx_userid)
        {
            return icollectService.GetCollect(wx_userid);

        }

        /// <summary>
        /// 添加收藏/删除收藏
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("changesCollect")]
        public int ChangesCollect(int userid, int novelid, bool iscollect)
        {
            int result = icollectService.ChangesCollect(userid,  novelid,  iscollect);
            return result;
        }

        [HttpGet]
        [Route("collection")]
        /// <summary>
        /// 判断是否收藏过该本小说
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="novelid"></param>
        /// <returns></returns>
        public int Collection(int userid, int novelid)
        {
            int result = icollectService.Collection(userid, novelid);
            return result;
        }
    }
}
