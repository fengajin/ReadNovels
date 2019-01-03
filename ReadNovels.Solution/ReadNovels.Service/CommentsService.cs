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
    /// 查看更多评论/吐槽
    /// </summary>
    public class CommentsService : ICommentsService
    {

        /// <summary>
        /// 添加小说评论
        /// </summary>
        /// <param name="comments">小说Id</param>
        /// <returns></returns>
        public int CommentsAdd(Comments comments)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string executeSql = @" INSERT INTO Comments(content, timea, novelid, userid ) VALUES ( :Content, :Timea, :Novelid, :Userid)";
                return conn.Execute(executeSql, comments);
            }
        }

        /// <summary>
        /// 获取此本小说所有评论
        /// </summary>
        /// <param name="Id">小说Id</param>
        /// <returns></returns>
        public List<Comments> GetComments(int Id)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string captureByIdSql = @"select id,content,timea,userid  from comments where novelid = :Id order by id desc";
                var conditon = new { Id = Id };
                var result = conn.Query<Comments>(captureByIdSql, conditon);
                return result.ToList();
            }
        }
    }
}
