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

    public class CollectService:ICollectService
    {
        /// <summary>
        /// 获取用户收藏的小说
        /// </summary>
        /// <param name="wx_userid"></param>
        /// <returns></returns>
        public List<Collect> GetCollect(int wx_userid)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                String sql = @"  select c.id,c.userid,c.novelid,n.NovelName,n.ImgPath,n.Intro,n.Author,n.Hits,n.NovelState,n.Num from collect c join wx_user u on c.userid=u.ID join novel n on c.novelid=n.id where c.userid=:id ";
                var Collectlist = new { id = wx_userid };
                var result = conn.Query<Collect>(sql, Collectlist).ToList();
                return result;
            }

        }
        /// <summary>
        /// 判断是否收藏过该本小说
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="novelid"></param>
        /// <returns></returns>
        public int Collection(int userid, int novelid)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql2 = @"select *from collect where userid=:userid and novelid=:novelid";
                var parameter2 = new { userid = userid, novelid = novelid };
                var collect = conn.Query<Collect>(sql2, parameter2).ToList();
                if (collect.Count == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }
        /// <summary>
        /// 添加收藏/删除收藏
        /// </summary>
        /// <returns></returns>
        public int ChangesCollect(int userid,int novelid,bool iscollect)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql;
                //如果是false 证明要删除，如果是true,证明要添加到小说到收藏表
                if (iscollect)
                {
                    string sql2 = @"select *from collect where userid=:userid and novelid=:novelid";
                    var parameter2 = new { userid = userid, novelid = novelid };
                    var collect= conn.Query<Collect>(sql2, parameter2).ToList();
                    if (collect.Count == 0)
                    {
                        sql = @"insert into collect(userid, novelid, createtime) values(:userid,:novelid,sysdate)";
                    }
                    else
                    {
                        return 0;
                    }
                   
                }
                else
                {
                    sql = @"delete from collect where userid=:userid and novelid=:novelid";
                }
                var parameter = new { userid = userid, novelid = novelid };
                var result = conn.Execute(sql,parameter);
                return result;

            }
        }
    }
}
