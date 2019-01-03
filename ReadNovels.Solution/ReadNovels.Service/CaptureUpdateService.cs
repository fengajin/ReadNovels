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
    /// 更新小说--数据访问层
    /// </summary>
    public class CaptureUpdateService : ICaptureUpdateService
    {

        /// <summary>
        /// 通过多条件进行查询
        /// </summary>
        /// <returns></returns>
        public List<Capture> GetCaptureByMore()
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = "select id, novelname, author, nums, graburl, websiteurl,isenabled, intro,novestate,createCapture, typed from Capture ";
                var result = conn.Query<Capture>(sql, null);
                return result.ToList();
            }
        }
    }
}
