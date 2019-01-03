using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.Service
{
    using Model;
    using Dapper;
    using Oracle.DataAccess.Client;
    using Common;
    using System.Data;

    /// <summary>
    /// 阅读记录表
    /// </summary>
    public class ReadsService
    {
        public List<Reads> GetReads(int wx_userid)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                String executeSql = @"select A.NOVELID,A.createtime,A.userid,A.CHAPTERID,B.NOVELNAME,B.IMGPATH,B.INTRO from (select userid，NOVELID,chapterid, createtime from (select a.*,row_number() over(partition by NOVELID order by createtime desc) rn  from reads a) c where rn = 1) A inner join novel B on A.NOVELID=B.id where A.USERID=:wx_userid and A.Createtime>(select  sysdate - interval '7' day  from dual)";
                var conditon = new { wx_userid = wx_userid };
                var readsList = conn.Query<Reads>(executeSql, conditon).ToList();
                return readsList;
            }

        }
    }
}
