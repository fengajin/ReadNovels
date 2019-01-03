using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReadNovels.WebApi.Controllers
{
    using ReadNovels.Service;
    using ReadNovels.Model;
    using ReadNovels.IService;

    /// <summary>
    /// 小说上架
    /// </summary>
    [RoutePrefix("novelAdded")]
    public class NovelAddedController : ApiController
    {
        private const int PageCount = 5;//每页显示几条
        INovelAddedService inovelAddedService = null;
        public NovelAddedController(INovelAddedService novelAddedService)
        {
            inovelAddedService = novelAddedService;
        }

        [HttpGet]
        [Route("novelAdded")]
        public Page NovelAdded(string NovelName, string IFShelf, int pages = 1)
        {
            //return inovelsService.GetNovelsAll();
            List<Novel> novels = inovelAddedService.NovelAdded();
            Page p = new Page();
            if (!string.IsNullOrWhiteSpace(NovelName))
            {
                novels = novels.Where(n => n.NovelName.Contains(NovelName)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(IFShelf))
            {
                int i = Convert.ToInt32(IFShelf);
                novels = novels.Where(n => n.IFShelf == i).ToList();
            }
            p.Currentpage = pages;
            p.Totalpage = (novels.Count / PageCount) + (novels.Count % PageCount == 0 ? 0 : 1);
            p.Data = novels.Skip((pages - 1) * PageCount).Take(PageCount);
            return p;
        }

        [Route("uptdateed")]
        [HttpGet]
        public int Uptdate(int Id, int IFShelf)
        {
            return inovelAddedService.Uptdate(Id, IFShelf);
        }

    }
}
