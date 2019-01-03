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
    
    /// <summary>
    /// 查看更多评论/吐槽WebApi
    /// </summary>
    [RoutePrefix("commentsWebApi")]
    public class CommentsWebApiController : ApiController
    {

        ICommentsService _iCommentsService = null;
        public CommentsWebApiController(ICommentsService iCommentsService)
        {
            _iCommentsService = iCommentsService;
        }
        
        /// <summary>
        /// 添加小说评论
        /// </summary>
        /// <param name="comments">小说Id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("commentsAdd")]
        public int CommentsAdd(Comments comments)
        {
            var result = _iCommentsService.CommentsAdd(comments);
            return result;
        }

        /// <summary>
        /// 获取此本小说所有评论
        /// </summary>
        /// <param name="Id">小说Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getComments")]
        public List<Comments> GetComments(int Id)
        {
            var result = _iCommentsService.GetComments(Id);
            return result;
        }
        
    }
}
