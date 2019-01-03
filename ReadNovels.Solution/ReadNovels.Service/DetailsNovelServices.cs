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
    using System.Collections;

    /// <summary>
    /// 小说详情Service
    /// </summary>
    public class DetailsNovelServices : IDetailsNovelServices
    {
        /// <summary>
        /// 获取小说详情
        /// </summary>
        /// <param name="Id">小说ID</param>
        /// <returns></returns>
        public List<Novel> GetDetailsNovels(int Id)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select Id, NovelName, ImgPath, Author, Num, NovelState, Hits, Intro from Novel where Id = :Id";
                var NovelList = new { Id = Id };
                var result = conn.Query<Novel>(sql, NovelList).ToList();
                return result;
            }
        }

        /// <summary>
        /// 获取小说最新章节
        /// </summary>
        /// <param name="Id">小说ID</param>
        /// <returns></returns>
        public List<Chapter> GetChapters(int Id)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select id,ChapterName,updatetime,novelid from Chapter where id = (
select Max(id) from Chapter where novelid = :Id)";
                var ChapterList = new { Id = Id };
                var result = conn.Query<Chapter>(sql, ChapterList).ToList();
                return result;
            }
        }

        /// <summary>
        /// 获取小说评价
        /// </summary>
        /// <param name="Id">小说ID</param>
        /// <returns></returns>
        public List<Comments> GetComments(int Id)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                //string sql = @"select Userid, Content from Comments where Novelid = Id and rownum=1 order by UpdateTime";
                //string sql = @"select ChapterName, UpdateTime from Chapter where Id = :Id and ChapterNum = (select MAX(ChapterNum) from Chapter where Novelid = :Id)";
                string sql = @"select id,content,userid from comments where novelid = :Id and rownum <= 3";
                var ChapterList = new { Id = Id };
                var result = conn.Query<Comments>(sql, ChapterList).ToList();
                return result;
            }
        }
        /// <summary>
        /// 添加到书架
        /// </summary>
        /// <param name="bookrack">书架表</param>
        /// <returns></returns>
        public int BookrackAdd(Bookrack bookrack)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                
                string captureByIdSql = @"INSERT INTO Bookrack ( UserId, NovelId, CreateTime, ModifyTime ) VALUES (  :UserId, :NovelId, :CreateTime, :ModifyTime )";
                var result = conn.Execute(captureByIdSql, bookrack);
                return result;
            }
        }

        /// <summary>
        /// 判断是否加入书架
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="novelid"></param>
        /// <returns></returns>
        public int IsBookRack(int userid, int novelid)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql2 = @"select * from bookrack where userid=:userid and novelid=:novelid";
                var conditon = new { userid = userid, novelid = novelid };
                var result = conn.Query<Bookrack>(sql2, conditon).ToList();
                if (result.Count == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

    }
}
