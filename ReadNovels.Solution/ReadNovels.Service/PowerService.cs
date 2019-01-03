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

    public class PowerService : IPowerService
    {
        public int AddPowers(Power p)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string executeSql = @" INSERT INTO Power (PowerName,CreateTime,Url,IsSelf) VALUES (:PowerName,:CreateTime,:Url,:IsSelf) ";
                p.CreateTime = System.DateTime.Now;
                var Collectlist = new {PowerName= p.PowerName, CreateTime = p.CreateTime , Url = p.Url, IsSelf=p.IsSelf };
                int result = conn.Execute(executeSql, Collectlist);
                return result;
            }
        }

        public int DeletePower(int Id)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string executeSql = @"delete from Power where Id=:Id";
                var Collectlist = new { Id = Id };
                int result = conn.Execute(executeSql, Collectlist);
                return result;
            }
        }

        public List<Power> GetPowers()
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select * from Power";

                var result = conn.Query<Power>(sql, null);
                return result.ToList();

            }
        }

        public List<Power> GetPowersById(int Id)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select * from Power where Id=:Id";
                var Collectlist = new {Id=Id };
                var result = conn.Query<Power>(sql, Collectlist);
                return result.ToList();
            }
        }

        public int UpdatePowers(Power p)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"Update Power set PowerName=:PowerName,ModifyTime=:ModifyTime,Url=:Url where Id=:Id";
                p.ModifyTime = System.DateTime.Now;
                var Collectlist = new { Id = p.Id, PowerName= p.PowerName, Url= p.Url , ModifyTime = p.ModifyTime };
                var result = conn.Execute(sql, Collectlist);
                return result;
            }
        }
    }
}
