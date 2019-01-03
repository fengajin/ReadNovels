using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ReadNovels.WinService.Model
{
    /// <summary>
    /// 抓取小说表
    /// </summary>
    public class Capture
    {
        /// <summary>
        /// 小说id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 小说名称
        /// </summary>
        public string NovelName { get; set; }
        /// <summary>
        /// 小说作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 小说字数
        /// </summary>
        public string Nums { get; set; }
        /// <summary>
        /// 小说路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 小说封面路径
        /// </summary>
        public string ImgPath { get; set; }
        /// <summary>
        /// 抓取网址
        /// </summary>
        public string Graburl { get; set; }
        /// <summary>
        /// 所在网站
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 抓取内容网站地址
        /// </summary>
        public string WebsiteURL { get; set; }
        /// <summary>
        /// 开始抓取时间
        /// </summary>
        public DateTime? CreateCapture { get; set; }
        /// <summary>
        /// 是否开始抓取(已开始1、未0)
        /// </summary>
        public int ISCapture { get; set; }
        /// <summary>
        /// 是否启用(已启用1、未0)
        /// </summary>
        public int ISEnabled { get; set; }
        /// <summary>
        /// 审核状态(已审核1、未0)
        /// </summary>
        public int Isverify { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Intro { get; set; }
        /// <summary>
        /// 小说状态(连载1、完结0)
        /// </summary>
        public int NoveState { get; set; }
        /// <summary>
        /// 小说类别
        /// </summary>
        public string Typed { get; set; }
    }
}
