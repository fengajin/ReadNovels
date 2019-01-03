using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.IService
{
    using ReadNovels.Model;
    /// <summary>
    /// 小说抓取、保存接口
    /// </summary>
    public interface IBookRackService
    {
        
        /// <summary>
        /// 获取书架表中数据
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        List<Novel> GetBookracks(int UserId);

        /// <summary>
        /// 书架图书删除根据Id
        /// </summary>
        /// <param name="Id">书架表Id</param>
        /// <returns></returns>
        int BookrackDelete(string Id);
        
    }
}
