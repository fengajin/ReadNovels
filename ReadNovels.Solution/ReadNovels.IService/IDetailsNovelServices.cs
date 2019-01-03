using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.IService
{
    using ReadNovels.Model;
    /// <summary>
    /// 小说详情接口
    /// </summary>
    public interface IDetailsNovelServices
    {

        /// <summary>
        /// 根据小说Id查找详情
        /// </summary>
        /// <param name="Id">小说ID</param>
        /// <returns></returns>
        List<Novel> GetDetailsNovels(int Id);

        /// <summary>
        /// 获取小说最新章节
        /// </summary>
        /// <param name="Id">小说ID</param>
        /// <returns></returns>
        List<Chapter> GetChapters(int Id);

        /// <summary>
        /// 获取小说评价
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<Comments> GetComments(int Id);
        /// <summary>
        /// 添加到书架
        /// </summary>
        /// <param name="bookrack">书架表</param>
        /// <returns></returns>
        int BookrackAdd(Bookrack bookrack);
        /// <summary>
        /// 判断是否加入书架
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="novelid"></param>
        /// <returns></returns>
        int IsBookRack(int userid, int novelid);

    }
}
