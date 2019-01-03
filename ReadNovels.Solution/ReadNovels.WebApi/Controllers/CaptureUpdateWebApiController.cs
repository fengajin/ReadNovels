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
    /// 小说抓取更新WebApi
    /// </summary>
    [RoutePrefix("captureUpdate")]
    public class CaptureUpdateWebApiController : ApiController
    {
        private const int PageCount = 5;//每页显示数

        ICaptureUpdateService _captureUpdateService = null;
        /// <summary>
        /// 构造函数注入点
        /// </summary>
        /// <param name="bookScarkService"></param>
        public CaptureUpdateWebApiController(ICaptureUpdateService captureUpdateService)
        {
            _captureUpdateService = captureUpdateService;
        }

        /// <summary>
        /// 通过多条件进行查询/分页
        /// </summary>
        /// <param name="NovelName">名称</param>
        /// <param name="Typed">小说类型</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getCaptureByMore")]
        public Page GetCaptureByMore( string NovelName = "", string Typed = "", int pages = 1)
        {
            //return inovelsService.GetNovelsAll();
            List<Capture> capture = _captureUpdateService.GetCaptureByMore();
            Page p = new Page();
            if (!string.IsNullOrWhiteSpace(NovelName))
            {
                capture = capture.Where(n => n.NovelName.Contains(NovelName)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(Typed) && Typed != "全部")
            {
                string strTyped = Typed;
                capture = capture.Where(n => n.Typed == strTyped).ToList();
            }
            p.Currentpage = pages;
            p.Totalpage = (capture.Count / PageCount) + (capture.Count % PageCount == 0 ? 0 : 1);
            p.Data = capture.Skip((pages - 1) * PageCount).Take(PageCount);
            return p;
        }
        
        /// <summary>
        /// 通过多条件进行查询/分页
        /// </summary>
        /// <param name="NoveState">是否连载</param>
        /// <param name="NovelName">小说名称</param>
        /// <param name="Typed">小说类型</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getCaptureCount")]
        public int GetCaptureCount()
        {
            List<Capture> capture = _captureUpdateService.GetCaptureByMore();
            var count = capture.Count;
            return count;
        }

    }
}
