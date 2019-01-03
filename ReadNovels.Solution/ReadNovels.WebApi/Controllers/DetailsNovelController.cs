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
    using ReadNovels.IService;

    /// <summary>
    /// 小说详情Api
    /// </summary>
    [RoutePrefix("detailsNovel")]
    public class DetailsNovelController : ApiController
    {
        IDetailsNovelServices _iDetailsNovelServices = null;
        public DetailsNovelController(IDetailsNovelServices iDetailsNovelServices)
        {
            _iDetailsNovelServices = iDetailsNovelServices;
        }

        /// <summary>
        /// 小说详情
        /// </summary>
        /// <param name="Id">小说Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getDetailsNovel")]
        public List<Novel> GetDetailsNovel(int Id)
        {
            var result = _iDetailsNovelServices.GetDetailsNovels(Id);
            return result;
        }

        /// <summary>
        /// 小说最新章节
        /// </summary>
        /// <param name="Id">小说Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getChapters")]
        public List<Chapter> GetChapters(int Id)
        {
            var result = _iDetailsNovelServices.GetChapters(Id);
            return result;
        }

        /// <summary>
        /// 小说前三评论
        /// </summary>
        /// <param name="Id">小说Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getComments")]
        public List<Comments> GetComments(int Id)
        {
            var result = _iDetailsNovelServices.GetComments(Id);
            return result;
        }

        /// <summary>
        /// 添加到书架
        /// </summary>
        /// <param name="bookrack">书架表</param>
        /// <returns></returns>
        [HttpPost]
        [Route("bookrackAdd")]
        public int BookrackAdd(Bookrack bookrack)
        {
            var result = _iDetailsNovelServices.BookrackAdd(bookrack);
            return result;
        }

        /// <summary>
        /// 判断是否加入书架
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="novelid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("isBookRack")]
        public int IsBookRack(int userid, int novelid)
        {
            var result = _iDetailsNovelServices.IsBookRack(userid, novelid);
            return result;
        }
    }
}
