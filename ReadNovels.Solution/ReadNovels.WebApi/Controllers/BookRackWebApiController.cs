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
    using System.IO;
    using System.Text.RegularExpressions;
    using ReadNovels.Common;
    using System.Text;

    /// <summary>
    /// 小说书架WebApi
    /// </summary>
    [RoutePrefix("bookrack")]
    public class BookRackWebApiController : ApiController
    {

        IBookRackService _bookRackService = null;
        public BookRackWebApiController(IBookRackService bookRackService)
        {
            _bookRackService = bookRackService;
        }
        
        /// <summary>
        /// 获取书架表中数据
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getbookracks")]
        public List<Novel> GetBookracks(int UserId)
        {
            var result = _bookRackService.GetBookracks(UserId);
            return result;
        }

        /// <summary>
        /// 书架图书删除根据Id
        /// </summary>
        /// <param name="Id">书架表Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("bookrackdelete")]
        public int BookrackDelete(string Id)
        {
            var result = _bookRackService.BookrackDelete(Id);
            return result;
        }

    }
}
