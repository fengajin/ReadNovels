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
    public  class NovelAddedService:INovelAddedService
    {
        public List<Novel> NovelAdded()
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select * from Novel";
                var result = conn.Query<Novel>(sql, null);
                return result.ToList();
            }
        }

        public int Uptdate(int Id, int IFShelf)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"update novel set IFShelf=:IFShelf where Id=:Id";
                var Collectlist = new { Id = Id, IFShelf = IFShelf };
                int i = conn.Execute(sql, Collectlist);
                return i;
            }

        }


    }
}
