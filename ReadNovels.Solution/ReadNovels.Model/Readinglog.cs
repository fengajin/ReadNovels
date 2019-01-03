using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.Model
{
    /// <summary>
    /// 阅读日志表
    /// </summary>
    public class Readinglog
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 小说Id
        /// </summary>
        public int Novelid { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Userid { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 小说名称
        /// </summary>
        public string NovelName { get; set; }
        /// <summary>
        /// 小说阅读量
        /// </summary>
        public int maxnum{ get; set; }
}
}
