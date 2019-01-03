using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.Model
{
    /// <summary>
    /// 小说表
    /// </summary>
    public class Novel
    {
        /// <summary>
        /// 主键Id
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
        public string Num { get; set; }
        /// <summary>
        /// 小说路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 小说封面路径
        /// </summary>
        public string ImgPath { get; set; }
        /// <summary>
        /// 小说状态(连载/完结)
        /// </summary>
        public int NovelState { get; set; }
        /// <summary>
        /// 是否热门
        /// </summary>
        public int Whether { get; set; }
        /// <summary>
        /// 点击量
        /// </summary>
        public int Hits { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Typeids { get; set; }
        /// <summary>
        /// 上下架状态(0下架,1上架)
        /// </summary>
        public int IFShelf { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// NovelId
        /// </summary>
        public int NovelId { get; set; }
        
        /// <summary>
        /// 小程序删除所有
        /// </summary>
        public bool Checked { get; set; }
    }
}
