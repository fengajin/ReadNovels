using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.IService
{
    using ReadNovels.Model;
   public interface IReadService
    {
       
        List<Chapter> CatalogShow(int novelid);
        Chapter ReadsingShow(int userid, int novelid);
        /// <summary>
        /// 点击下一章
        /// </summary>
        /// <returns></returns>
        Chapter NextChapter(int userid, int novelid, int chapterid);
        /// <summary>
        /// 点击上一章
        /// </summary>
        /// <returns></returns>
         Chapter PrevChapter(int userid, int novelid, int chapterid);
        /// <summary>
        /// 点击目录里面的章节
        /// </summary>
        /// <returns></returns>
        Chapter Catalog_Chapter(int userid, int novelid, int chapterid);
    }
}
