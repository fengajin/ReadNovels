using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.IService
{
    using ReadNovels.Model;
    /// <summary>
    /// 查看更多评论/吐槽
    /// </summary>
    public interface ICommentsService
    {
        /// <summary>
        /// 获取此本小说所有评论
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<Comments> GetComments(int Id);

        /// <summary>
        /// 添加小说评论
        /// </summary>
        /// <param name="comments"></param>
        /// <returns></returns>
        int CommentsAdd(Comments comments);

    }
}
