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
    public class BookScarkService : IBookScarkService
    {
        /// <summary>
        /// 显示所有小说
        /// </summary>
        /// <returns></returns>
        public List<Novel> GetNovelsAll()
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select * from Novel where IFShelf = 1";
                var result = conn.Query<Novel>(sql, null);
                return result.ToList();
            }
        }
        /// <summary>
        /// 通过类别进行查询小说
        /// </summary>
        /// <param name="typeids"></param>
        /// <returns></returns>
        public List<Novel> GetNovelsByIds(string typeids)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                String sql = @" select * from novel where Typeids = :typeids and IFShelf = 1 ";
                var conditon = new { Typeids = typeids };
                var result = conn.Query<Novel>(sql, conditon);
                return result.ToList();
            }
        }

        /// <summary>
        /// 通过是否连载查询
        /// </summary>
        /// <param name="NovelState"></param>
        /// <returns></returns>
        public List<Novel> GetNovelsByNovelState(int NovelState)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                String sql = @" select * from novel where NovelState = :NovelState and IFShelf = 1 ";
                var conditon = new { NovelState = NovelState };
                var result = conn.Query<Novel>(sql, conditon);
                return result.ToList();
            }
        }
    }
}
