using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.IService
{
    using ReadNovels.Model;
   public interface ICollectService
    {
        /// <summary>
        /// 查看收藏
        /// </summary>
        /// <param name="wx_userid"></param>
        /// <returns></returns>
        List<Collect> GetCollect(int wx_userid);
        /// <summary>
        /// 添加收藏/删除收藏
        /// </summary>
        /// <returns></returns>
        int ChangesCollect(int userid, int novelid, bool iscollect);
        /// <summary>
        /// 判断是否收藏过该本小说
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="novelid"></param>
        /// <returns></returns>
        int Collection(int userid, int novelid);
    }
}
