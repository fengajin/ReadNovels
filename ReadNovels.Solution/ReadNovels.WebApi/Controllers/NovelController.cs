using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ReadNovels.Service;

namespace ReadNovels.WebApi.Controllers
{
    using ReadNovels.Model;
    using ReadNovels.IService;

    [RoutePrefix("novel")]
    public class NovelController : ApiController
    {
        private const int PageCount= 5;
        INovelsService inovelsService = null;
        public NovelController(INovelsService novelsService)
        {
            inovelsService = novelsService;
        }

        /// <summary>
        /// 获取微信精选
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getNovelsAll")]
        public List<Novel> GetNovelsAll()
        {
            var result = new ReadNovels.Service.NovelsService().GetNovelsAll();
            return result;
        }

        /// <summary>
        /// 获取男生小说
        /// </summary>
        /// <returns></returns>
        [Route("getNovelsMan")]
        public List<Novel> GetNovelsMan()
        {
            return inovelsService.GetNovelsMan();
        }

        /// <summary>
        /// 获取女生小说
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getNovelsMen")]
        public List<Novel> GetNovelsMen()
        {
            return inovelsService.GetNovelsMen();
        }

        /// <summary>
        /// 查看更多男生小说
        /// </summary>
        /// <returns></returns>
        [Route("getNovelsManMore")]
        public List<Novel> GetNovelsManMore()
        {
            return inovelsService.GetNovelsManMore();
        }

        /// <summary>
        /// 查看更多女生小说
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getNovelsMenMore")]
        public List<Novel> GetNovelsMenMore()
        {
            return inovelsService.GetNovelsMenMore();
        }
        
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="NovelName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("searchPages")]
        public List<Novel> SearchPages(string NovelName)
        {
            return inovelsService.SearchPages(NovelName);
        }
    }
}
