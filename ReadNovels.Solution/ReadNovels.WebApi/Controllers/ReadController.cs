using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReadNovels.WebApi.Controllers
{
    using ReadNovels.Model;
    using ReadNovels.Service;
    using ReadNovels.IService;
    using System.IO;
    using System.Text;
    
    /// <summary>
    /// 阅读小说
    /// </summary>
    [RoutePrefix("read")]
    public class ReadController : ApiController
    {
        IReadService ireadService = null;
        public ReadController(IReadService readService)
        {
            ireadService = readService;
        }
        
        /// <summary>
        /// 获取目录
        /// </summary>
        /// <param name="novelid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("catalogShow")]
        public List<Chapter> CatalogShow(int novelid)
        {
            return ireadService.CatalogShow(novelid);
        }

        /// <summary>
        /// 获取小说的内容
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="novelid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("readsingShow")]
       public Chapter ReadsingShow(int userid, int novelid)
        {
            var chapter = ireadService.ReadsingShow(userid, novelid);
            string str = Getfonts(chapter.ChapterURL);
            chapter.ChapterContent = str;
            return chapter;
        }

        /// <summary>
        /// 得到章节内容 文字
        /// </summary>
        /// <returns></returns>
        public string Getfonts(string chapterURL)
        {
            var imgurl = System.Web.Hosting.HostingEnvironment.MapPath(@"~/") + chapterURL;
            // var imgurl = System.Web.Hosting.HostingEnvironment.MapPath(@"~/") + "Novels\\钢铁是怎么样练成的\\第二章.txt";
            //1初始化   打开文件,不存在则创建并打开
            FileStream fs = System.IO.File.Open(imgurl, FileMode.Open);
            //创建字节数组b
            byte[] b = new byte[fs.Length];
            //读取文件到数组b,长度是b的长度
            fs.Read(b, 0, b.Length);
            //关闭
            fs.Close();
            //将字节数组内容转化为字符串
            string str = Encoding.UTF8.GetString(b);
            return str;
        }

        /// <summary>
        /// 点击下一章
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("nextChapter")]
        public Chapter NextChapter(int userid, int novelid, int chapterid)
        {
            var chapter= ireadService.NextChapter(userid, novelid, chapterid);
            string str = Getfonts(chapter.ChapterURL);
            chapter.ChapterContent = str;
            return chapter;
        }

        /// <summary>
        /// 点击上一章
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("prevChapter")]
        public Chapter PrevChapter(int userid, int novelid, int chapterid)
        {
            var chapter = ireadService.PrevChapter(userid, novelid, chapterid);
            string str = Getfonts(chapter.ChapterURL);
            chapter.ChapterContent = str;
            return chapter;
        }

        /// <summary>
        /// 点击目录里面的章节
        /// </summary>
        /// <returns></returns>
        [Route("catalog_Chapter")]
        [HttpGet]
        public Chapter Catalog_Chapter(int userid, int novelid, int chapterid)
        {
            var chapter = ireadService.Catalog_Chapter(userid, novelid, chapterid);
            string str = Getfonts(chapter.ChapterURL);
            chapter.ChapterContent = str;
            return chapter;
        }
        
    }
}
