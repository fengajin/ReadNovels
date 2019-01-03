using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.Model
{
    /// <summary>
    /// 收藏表
    /// </summary>
    public class Collect
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
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
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
        /// 小说状态(连载/完结)
        /// </summary>
        public int NovelState { get; set; }
        /// <summary>
        /// 点击量
        /// </summary>
        public int Hits { get; set; }
        /// <summary>
        /// 小说作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 小说字数
        /// </summary>
        public string Num { get; set; }
    }
}
