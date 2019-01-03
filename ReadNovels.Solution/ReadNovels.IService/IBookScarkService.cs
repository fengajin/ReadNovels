using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReadNovels.Model;

namespace ReadNovels.IService
{
    public interface IBookScarkService
    {
        /// <summary>
        /// 显示所有小说
        /// </summary>
        /// <returns></returns>
        List<Novel> GetNovelsAll();

        /// <summary>
        /// 通过类别查询小说
        /// </summary>
        /// <param name="typeids"></param>
        /// <returns></returns>
        List<Novel> GetNovelsByIds(string typeids);

        /// <summary>
        /// 通过是否连载进行查询
        /// </summary>
        /// <param name="NovelState"></param>
        /// <returns></returns>
        List<Novel> GetNovelsByNovelState(int NovelState);
    }
}
