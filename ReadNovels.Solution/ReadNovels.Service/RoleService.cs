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

    public class RoleService : IRoleService
    {
        public int AddRole(Role r)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string executeSql = @" INSERT INTO Role (RoleName,PowerId,PowerName,CreateTime,ModifyTime) VALUES (:RoleName,:PowerId,:PowerName,:CreateTime,:ModifyTime)";
                r.CreateTime = System.DateTime.Now;
                r.ModifyTime= System.DateTime.Now; 
                var Collectlist = new { RoleName = r.RoleName, PowerId = r.PowerId, PowerName = r.PowerName, CreateTime = r.CreateTime, ModifyTime=r.ModifyTime };
                int result = conn.Execute(executeSql, Collectlist);
                if (result > 0)
                {
                    string sql = @"select Id from Role where RoleName=:rowname";
                    var a = new{ rowname = r.RoleName };
                    var Id = conn.Query(sql, a).FirstOrDefault();
                    var PowerId = r.PowerId.Split(',');
                    for (int i = 0; i < PowerId.Length; i++)
                    {
                        RoleAction roleAction = new RoleAction();
                        roleAction.RoleId = int.Parse(Id.Values.FirstOrDefault().ToString());
                        roleAction.PowerId = Convert.ToInt32(PowerId[i]);
                        roleAction.CreateTime = System.DateTime.Now;
                        roleAction.ModifyTime = System.DateTime.Now;
                        string sql1 = @"insert into RoleAction (RoleId,PowerId,CreateTime,ModifyTime) VALUES (:RoleId,:PowerId,:CreateTime,:ModifyTime)";
                        int result1 = conn.Execute(sql1, roleAction);
                    }
                }
                return result;
            }
        }

        public int DeleteRole(int Id)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string executeSql = @"delete from Role where Id=:Id";
                var Collectlist = new { Id = Id };
                int result = conn.Execute(executeSql, Collectlist);
                if (result > 0)
                {
                    string executeSqls = @"delete from RoleAction where RoleId=:Id";
                    var Collectlists = new { Id = Id };
                    int results = conn.Execute(executeSqls, Collectlists);
                }
                return result;
            }
        }

        public List<Role> GetRole()
          {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select * from Role";

                var result = conn.Query<Role>(sql, null);
                return result.ToList();

            }
          }

        public List<Role> GetRoleById(int Id)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select * from Role where Id=:Id";
                var Collectlist = new { Id = Id };
                var result = conn.Query<Role>(sql, Collectlist);
                return result.ToList();
            }
        }

        public int UpdateRole(Role role)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string executeSql = @" Update Role set RoleName=:RoleName,PowerId=:PowerId,PowerName=:PowerName,ModifyTime=:ModifyTime where Id=:Id";
                role.ModifyTime = System.DateTime.Now;
                var Collectlist = new { RoleName = role.RoleName, PowerId = role.PowerId, PowerName = role.PowerName, ModifyTime = role.ModifyTime, Id=role.Id };
                int result = conn.Execute(executeSql, Collectlist);
                if (result > 0)
                {
                    string sql = @"select Id from Role where RoleName=:rowname";
                    var a = new { rowname = role.RoleName };
                    var Ids = conn.Query(sql, a).FirstOrDefault();
                    string executeSqls = @"delete from RoleAction where RoleId=:Id";
                    var Collectlists = new { Id = role.Id };
                    int results = conn.Execute(executeSqls, Collectlists);
                    var PowerIds = role.PowerId.Split(',');
                    for (int i = 0; i < PowerIds.Length; i++)
                    {
                        RoleAction roleAction = new RoleAction();
                        roleAction.RoleId = int.Parse(Ids.Values.FirstOrDefault().ToString());
                        roleAction.PowerId = Convert.ToInt32(PowerIds[i]);
                        roleAction.CreateTime = System.DateTime.Now;
                        roleAction.ModifyTime = System.DateTime.Now;
                        string sql1 = @"insert into RoleAction (RoleId,PowerId,CreateTime,ModifyTime) VALUES (:RoleId,:PowerId,:CreateTime,:ModifyTime)";
                        int result1 = conn.Execute(sql1, roleAction);
                    }
                }
                return result;
            }
        }
    }
}
