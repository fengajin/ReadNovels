using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.Model
{
    /// <summary>
    /// 抓取小说章节表
    /// </summary>
    public class CaptureSection
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 抓取小说表Id
        /// </summary>
        public int Captureid { get; set; }

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
        /// 抓取时间
        /// </summary>
        public DateTime? CaptureTime { get; set; }
        /// <summary>
        /// 小说路径
        /// </summary>
        public string Path { get; set; }
    }
}
