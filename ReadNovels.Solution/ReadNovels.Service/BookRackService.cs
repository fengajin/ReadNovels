using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.Service
{
    using ReadNovels.IService;
    using ReadNovels.Model;
    using ReadNovels.Common;
    using Dapper;
    using Oracle.DataAccess.Client;

    /// <summary>
    /// 书架Service
    /// </summary>
    public class BookRackService : IBookRackService
    {

        /// <summary>
        /// 获取书架表中数据
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public List<Novel> GetBookracks(int UserId)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string captureByIdSql = @"select n.Id as NovelId, n.NovelName, n.ImgPath, b.id  from Novel n join Bookrack b on n.Id = b.novelid and b.userid = :UserId";
                var conditon = new { UserId = UserId };
                var result = conn.Query<Novel>(captureByIdSql, conditon);
                foreach (var a in result.ToList())
                {
                    a.Checked = false;
                }
                return result.ToList();
            }
        }

        /// <summary>
        /// 书架图书删除根据Id
        /// </summary>
        /// <param name="Id">书架表Id</param>
        /// <returns></returns>
        public int BookrackDelete(string Id)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                //string captureByIdSql = @"delete from BOOKRACK where Id = :Id";
                // string captureByIdSql = @"delete from bookrack where  Id in(:Id)";
                string captureByIdSql = string.Format("delete from bookrack where  Id in({0})", Id);
                //var conditon = new { Id = Id };
                var result = conn.Execute(captureByIdSql, null);
                return result;
            }
        }

       

       
    }
}
