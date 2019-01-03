using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReadNovels.IService;
using ReadNovels.Model;
using ReadNovels.Common;
using Dapper;
using Oracle.DataAccess.Client;

namespace ReadNovels.Service
{
    public class NovelAuditService : INovelAuditService
    {
        /// <summary>
        /// 修改抓取小说表审批状态
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int AuditNovel(string ids)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                var result = ids.Split(',');
                List<Novel> listNovel = new List<Novel>();
                Novel novelModel;

                List<Chapter> listChapter = new List<Chapter>();


                foreach (var item in result)
                {

                    novelModel = GetNovelById(int.Parse(item));
                    listChapter.AddRange(GetCapturesById(int.Parse(item)));
                    listNovel.Add(novelModel);
                }

                int i = Add(listNovel, listChapter);
                if (i > 0)
                {
                    foreach (var item in result)
                    {
                        string sql = @"update capture set Isverify=1 where Id=:Id";
                        var conditon1 = new { Id = item };
                        var result1 = conn.Execute(sql, conditon1);
                    }

                }
                return i;
            }

        }




        /// <summary>
        /// 通过Id获取章节
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<Chapter> GetCapturesById(int Id)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
              
                string sql1 = @"select * from CaptureSection where Captureid = :Id ";
                var conditon1 = new { Id = Id };
                List<CaptureSection> capturesection = conn.Query<CaptureSection>(sql1, conditon1).ToList();
                List<Chapter> listchapter = new List<Chapter>();
                Chapter chapter;
                foreach (var item in capturesection)
                {
                    chapter = new Chapter();
                    chapter.Id = item.Id;
                    chapter.Novelid = item.Captureid;
                    chapter.ChapterName = item.ChapterName;
                    chapter.ChapterNum = item.ChapterNum;
                    chapter.ChapterURL = item.ChapterURL;
                    listchapter.Add(chapter);
                }
                

             
                return listchapter;

            }
        }

        /// <summary>
        /// 通过Id获取小说并存放在model
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Novel GetNovelById(int Id)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select * from Capture where Id = :Id ";
                var conditon = new { Id = Id };
                Capture capture = conn.Query<Capture>(sql, conditon).FirstOrDefault();

                Novel novelModel = new Novel();
                novelModel.Id = capture.Id;
                novelModel.NovelName = capture.NovelName;
                novelModel.Author = capture.Author;
                novelModel.Num = capture.Nums;
                novelModel.Path = capture.Path;
                novelModel.ImgPath = capture.ImgPath;
                novelModel.NovelState = capture.NoveState;
                novelModel.Typeids = capture.Typed;
                novelModel.IFShelf = 0;
                novelModel.Intro = capture.Intro;
                return novelModel;

            }
        }


        /// <summary>
        /// 批量添加小说和章节
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int Add(List<Novel> novellist,List<Chapter> chapterlist)
        {
         
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                conn.Open();
                //批量添加小说
                string executeSql = @" insert into novel(Id,NovelName,Author,num,path,imgpath,novelstate,typeids,ifshelf,intro) values(:Id,:NovelName,:Author,:Num,:Path,:ImgPath,:NovelState,:Typeids,:IFShelf,:Intro) ";
                int result1 = conn.Execute(executeSql, novellist);

                //批量添加章节
                string executeSql2 = @" insert into chapter(Id,Novelid,ChapterName,ChapterNum,ChapterURL) values(:Id,:Novelid,:ChapterName,:ChapterNum,:ChapterURL) ";
                int result2 = conn.Execute(executeSql2, chapterlist);


                return result1;

            }

        }

        /// <summary>
        /// 显示抓取小说表
        /// </summary>
        /// <returns></returns>
        public List<Capture> GetCaptures()
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select * from capture";
                var result = conn.Query<Capture>(sql, null);
                return result.ToList();
            }
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <returns></returns>
       





    }
}
