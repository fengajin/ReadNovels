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
    /// <summary>
    /// 小说审核WebApi
    /// </summary>
    [RoutePrefix("novelAudit")]
    public class NovelAuditController : ApiController
    {

        //页面尺寸
        private const int PageCount = 3;

        INovelAuditService _novelaudit = null;

        public NovelAuditController(INovelAuditService novelAuditService)
        {
            _novelaudit = novelAuditService;
        }

        /// <summary>
        /// 小说审核页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getCaptures")]
        public Page GetCaptures(string NovelName,int ?Isverify, int page = 1)
        {
            var result = _novelaudit.GetCaptures();
            Page p = new Page();
            if (!string.IsNullOrWhiteSpace(NovelName))
            {
                result = result.Where(r => r.NovelName.Contains(NovelName)).ToList();
            }
            if (Isverify!=null)
            {
                result = result.Where(r=>r.Isverify==Isverify).ToList();
            }
            p.Currentpage = page;
            p.Totalpage = (result.Count / PageCount) + (result.Count % PageCount == 0 ? 0 : 1);
            p.Data = result.Skip((page - 1) * PageCount).Take(PageCount);
            return p;
        }
        
        /// <summary>
        /// 修改抓取小说表审批状态
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("auditNovel")]
        public int AuditNovel(string ids)
        {
            var result = _novelaudit.AuditNovel(ids);
            return result;
            
        }

        

    }
}
