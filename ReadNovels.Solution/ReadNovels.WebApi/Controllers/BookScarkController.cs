using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ReadNovels.Model;
using ReadNovels.Service;
using ReadNovels.IService;

namespace ReadNovels.WebApi.Controllers
{
    [RoutePrefix("bookScark")]
    public class BookScarkController : ApiController
    {

        IBookScarkService _bookscarkservice = null;
        /// <summary>
        /// 构造函数注入点
        /// </summary>
        /// <param name="bookScarkService"></param>
        public BookScarkController(IBookScarkService bookScarkService)
        {
            _bookscarkservice = bookScarkService;
        }

        /// <summary>
        /// 显示所有小说
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllNovels")]
        public List<Novel> GetNovelsAll()
        {
            var result = _bookscarkservice.GetNovelsAll();
            return result;

        }

        /// <summary>
        /// 通过类别进行查询小说
        /// </summary>
        /// <param name="typeids"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetNovelsByIds")]
        public List<Novel> GetNovelsByIds(string typeids)
        {
            var result = _bookscarkservice.GetNovelsByIds(typeids);

            return result;

        }

        /// <summary>
        /// 通过是否连载查询
        /// </summary>
        /// <param name="NovelState"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetNovelsByNovelState")]
        public List<Novel> GetNovelsByNovelState(int NovelState)
        {
            var result = _bookscarkservice.GetNovelsByNovelState(NovelState);
            return result.ToList();
        }
    }
}
