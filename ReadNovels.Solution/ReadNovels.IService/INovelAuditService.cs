using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReadNovels.Model;

namespace ReadNovels.IService
{
    /// <summary>
    /// 小说审核页面接口
    /// </summary>
   public  interface INovelAuditService
    {
        List<Capture> GetCaptures();

        int AuditNovel(string ids);

        List<Chapter> GetCapturesById(int Id);

        Novel GetNovelById(int Id);

        int Add(List<Novel> novellist, List<Chapter> chapterlist);
    }
}
