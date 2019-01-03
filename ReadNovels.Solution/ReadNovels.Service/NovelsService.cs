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

    public class NovelsService : INovelsService
    {
        public List<Novel> GetNovelsAll()
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select * from Novel where IFShelf=1";
                var result = conn.Query<Novel>(sql, null);
                return result.ToList();
            }
        }
        public List<Novel> GetNovelsMan()
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select * from novel where TYPEIDS='玄幻奇幻' and IFShelf=1 or TYPEIDS='武侠仙侠' and IFShelf=1 or TYPEIDS='历史军事' and IFShelf=1 or TYPEIDS='网游竞技' and IFShelf=1";
                var result = conn.Query<Novel>(sql, null).ToList();
                return result;
            }
        }
        public List<Novel> GetNovelsManMore()
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select * from novel where TYPEIDS='玄幻奇幻' and IFShelf=1 or TYPEIDS='武侠仙侠' and IFShelf=1 or TYPEIDS='历史军事' and IFShelf=1 or TYPEIDS='网游竞技' and IFShelf=1";
                var result = conn.Query<Novel>(sql, null).ToList();
                return result;
            }
        }
        public List<Novel> GetNovelsMen()
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select * from novel where TYPEIDS='女频频道' and IFShelf=1 or TYPEIDS='都市言情' and IFShelf=1";
                var result= conn.Query<Novel>(sql, null).ToList();
                return result;
            }
        }
        public List<Novel> GetNovelsMenMore()
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select * from novel where TYPEIDS='女频频道' and IFShelf=1 or TYPEIDS='都市言情' and IFShelf=1";
                var result = conn.Query<Novel>(sql, null).ToList();
                return result;
            }
        }


       

        public List<Novel> SearchPages(string NovelName)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select * from novel where IFShelf=1";
                if (!string.IsNullOrEmpty(NovelName))
                {
                    sql += " and NovelName like:NovelName";
                }
                var para = new { NovelName = '%'+ NovelName + '%'};
                var result = conn.Query<Novel>(sql, para).ToList();
                return result;
            }             

        }
    }
}
