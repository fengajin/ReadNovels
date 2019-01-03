using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.Model
{
    /// <summary>
    /// 阅读记录表
    /// </summary>
    public class Reads
    {
        /// <summary>
        /// 主键Id
        /// </summary>
       public int Id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
       public int Userid { get; set; }
        /// <summary>
        /// 小说Id
        /// </summary>
       public int Novelid { get; set; }
        /// <summary>
        /// 章节Id
        /// </summary>
       public int Chapterid { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
       public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 小说名称
        /// </summary>
        public string NovelName { get; set; }
        /// <summary>
        /// 小说封面路径
        /// </summary>
        public string ImgPath { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Intro { get; set; }
        /// <summary>
        /// 章节名称
        /// </summary>
        public string ChapterName { get; set; }
        /// <summary>
        /// 章节url
        /// </summary>
        public string ChapterURL { get; set; }
    }
}
