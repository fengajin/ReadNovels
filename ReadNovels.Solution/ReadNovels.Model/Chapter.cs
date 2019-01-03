using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.Model
{
    /// <summary>
    /// 小说章节表
    /// </summary>
    public class Chapter
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
        /// 章节名称
        /// </summary>
        public string ChapterName { get; set; }

        /// <summary>
        /// 章节Number
        /// </summary>
        public int ChapterNum { get; set; }

        /// <summary>
        /// 章节url
        /// </summary>
        public string ChapterURL { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
       public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 章节内容
        /// </summary>
        public string ChapterContent { get; set; }
    }
}
