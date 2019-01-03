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
    public class TypeService : ITypeServices
    {
        public List<Types> GetTypes()
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select * from types";
                var result = conn.Query<Types>(sql, null);
                return result.ToList();
            }
        }
    }
}
