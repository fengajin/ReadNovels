using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.Service
{
    using Dapper;
    using Oracle.DataAccess.Client;
    using ReadNovels.Common;
    using ReadNovels.IService;
    using ReadNovels.Model;
    public class ReadinglogService : IReadinglogService
    {
        /// <summary>
        /// 获取指定时间段内的阅读量排前10的小说
        /// </summary>
        /// <param name="datetime1"></param>
        /// <param name="datetime2"></param>
        /// <returns></returns>
        public List<Readinglog> GetReadinglogs(DateTime? datetime1, DateTime? datetime2)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = "select b.novelname,a.maxnum from (select novelid,maxnum from (select novelid,count(id) as maxnum from Readinglog where createtime between to_date(to_char(:datetime1, 'yyyy-MM-dd'),'yyyy-mm-dd') and to_date(to_char(:datetime2, 'yyyy-MM-dd'),'yyyy-mm-dd') group by novelid order by count(id) desc) a where rownum <10) a inner join novel b on a.novelid=b.id";
                var conditon = new { datetime1 = datetime1, datetime2 = datetime2 };
                var readingloglist = conn.Query<Readinglog>(sql, conditon);
                return readingloglist.ToList();
            }


        }

        /// <summary>
        /// 进入小说详情页时加入阅读记录表
        /// </summary>
        /// <param name="Novelid"></param>
        /// <param name="Userid"></param>
        /// <returns></returns>
        public int GetReadinglogsById(int Novelid, int Userid)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                String sql = @" select * from Readinglog where Novelid = :Novelid and Userid = :Userid ";
                var conditon = new { Novelid = Novelid, Userid= Userid };
                var result = conn.Query<Readinglog>(sql, conditon);
                if (result.Count() > 0)
                {
                    return 1;
                }
                else
                {
                    
                   
                        String sql1 = @" insert into Readinglog(Novelid,Userid,CreateTime) values(:Novelid,:Userid,sysdate) ";
                        var conditon1 = new { Novelid = Novelid, Userid = Userid };
                        int result1 = conn.Execute(sql1, conditon1);


                    return result1;
                }
                
            }

            
        }


        
    }
}
