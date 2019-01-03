using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.Service
{
    using ReadNovels.IService;
    using ReadNovels.Model;
    using Common;
    using Dapper;
    using Oracle.DataAccess.Client;
   public class ReadService:IReadService
    {

        /// <summary>
        /// 获取目录
        /// </summary>
        /// <param name="novelid"></param>
        /// <returns></returns>
        public List<Chapter> CatalogShow(int novelid)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                //select * from Chapter where novelid=:id group by chapternum asc
                string sql = @"select * from Chapter where novelid=:id order by chapternum asc";
                var Collectlist = new { id = novelid };
                var result = conn.Query<Chapter>(sql, Collectlist);
                return result.ToList();
            }
        }
        /// <summary>
        /// 阅读界面加载的时候
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="novelid"></param>
        /// <returns></returns>

        public Chapter ReadsingShow(int userid, int novelid)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                //select NovelId,max(CreateTime) as CreateTime from [dbo].[Reads] a inner join chapter  b on a. where UserId = ？ and  NovelId=？
                //先查询这个用户是否看过这本小说
                var sql1 = "select  * from reads where userid=:userid and novelid=:novelid";
                var Collectlist1 = new { userid = userid, novelid = novelid };
                var result1 = conn.Query<Reads>(sql1, Collectlist1);

                if (result1.Count() > 0)
                {
                    //该用户看过阅读过这本小说，查出阅读的最新章节
                    string sql = @"select chapterurl,chapternum,chaptername,novelid from chapter where novelid=:novelid and chapternum=(select chapterid from reads where id=(select max(id) from Reads where userid=:userid and novelid=:novelid))";
                    var Collectlist = new { userid = userid, novelid = novelid };
                    var result = conn.Query<Chapter>(sql, Collectlist);
                    return result.FirstOrDefault();
                }
                else
                {
                    //该用户从来没有阅读过这本小说，向阅读记录表插入一条阅读记录，章节为第一章
                    string sqlAdd = @"insert into reads(userid, novelid, chapterid, createtime) values(:userid, :novelid, 0, sysdate)";
                    var Collectlist = new { userid = userid, novelid = novelid };
                    int i = conn.Execute(sqlAdd, Collectlist);

                    //查出该小说第一章所在的url
                    string Query = @"select id, novelid, chapterurl, chaptername,chapternum  from chapter  where chapternum=0 and novelid=:novelid";
                    var Collectlists = new { novelid = novelid };
                    var result = conn.Query<Chapter>(Query, Collectlists);
                    return result.FirstOrDefault();
                }
            }
        }

        /// <summary>
        /// 点击下一章
        /// </summary>
        /// <returns></returns>
        public Chapter NextChapter(int userid, int novelid,int chapterid)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                //查询章节表中的这个小说的总章节是多少，如果已经是最后一章，就返回空
                string sql1 = @"select  * from chapter where novelid=:novelid and chapternum=(select  max(chapternum) from chapter where novelid=:novelid )";
                var parameter = new { novelid=novelid };
                var listChapter= conn.Query<Chapter>(sql1, parameter).FirstOrDefault();
                if (listChapter.ChapterNum ==chapterid)
                {
                    return listChapter;
                }
                int ChapterNum = chapterid + 1;
                //向阅读记录表中插入章节num +1的数据
                string sqlAdd = @"insert into reads(userid, novelid, chapterid, createtime) values(:userid, :novelid, :chapterid, sysdate)";
                var Collectlist = new { userid = userid, novelid = novelid, chapterid = ChapterNum };
                int i = conn.Execute(sqlAdd, Collectlist);

                //查出该小说这一章所在的url
                string Query = @"select id, novelid, chapterurl, chaptername,chapternum  from chapter  where ChapterNum=:ChapterNum and novelid=:novelid";
                var Collectlists = new { novelid = novelid, ChapterNum = ChapterNum };
                var result = conn.Query<Chapter>(Query, Collectlists);
                 return result.FirstOrDefault();

            }
               
        }
        /// <summary>
        /// 点击上一章
        /// </summary>
        /// <returns></returns>
        public Chapter PrevChapter(int userid, int novelid, int chapterid)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                if (chapterid == 0)
                {
                    string sql1 = @"select   id, novelid, chapterurl, chaptername,chapternum from chapter where novelid=:novelid and chapternum=0";
                    var parameter = new { novelid = novelid };
                    var listChapter = conn.Query<Chapter>(sql1, parameter).FirstOrDefault();
                    return listChapter;
                }
                int ChapterNum = chapterid - 1;
                //向阅读记录表中插入章节num +1的数据
                string sqlAdd = @"insert into reads(userid, novelid, chapterid, createtime) values(:userid, :novelid, :chapterid, sysdate)";
                var Collectlist = new { userid = userid, novelid = novelid, chapterid = ChapterNum };
                int i = conn.Execute(sqlAdd, Collectlist);

                //查出该小说这一章所在的url
                string Query = @"select id, novelid, chapterurl, chaptername,chapternum  from chapter  where ChapterNum=:ChapterNum and novelid=:novelid";
                var Collectlists = new { novelid = novelid, ChapterNum = ChapterNum };
                var result = conn.Query<Chapter>(Query, Collectlists);
                return result.FirstOrDefault();

            }

        }
        /// <summary>
        /// 点击目录里面的章节
        /// </summary>
        /// <returns></returns>
        public Chapter Catalog_Chapter(int userid, int novelid, int chapterid)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                //向阅读记录表中插入章节num +1的数据
                string sqlAdd = @"insert into reads(userid, novelid, chapterid, createtime) values(:userid, :novelid, :chapterid, sysdate)";
                var Collectlist = new { userid = userid, novelid = novelid, chapterid = chapterid };
                int i = conn.Execute(sqlAdd, Collectlist);

                //查出该小说这一章所在的url
                string Query = @"select id, novelid, chapterurl, chaptername,chapternum  from chapter  where ChapterNum=:ChapterNum and novelid=:novelid";
                var Collectlists = new { novelid = novelid, ChapterNum = chapterid };
                var result = conn.Query<Chapter>(Query, Collectlists);
                return result.FirstOrDefault();

            }
              
        }




    }
}
