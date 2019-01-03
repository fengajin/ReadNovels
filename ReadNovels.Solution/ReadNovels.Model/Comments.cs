using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.Model
{
    /// <summary>
    /// 评价表
    /// </summary>
    public class Comments
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }
     
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? Timea { get; set; }
        /// <summary>
        /// 小说Id
        /// </summary>
        public int Novelid { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Userid { get; set; }
    }
}
