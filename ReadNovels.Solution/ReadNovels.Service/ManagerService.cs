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
    public class ManagerService : IManagerService
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="managers"></param>
        /// <returns></returns>
        public int AddManagers(Managers managers)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string executeSql = @" INSERT INTO Managers (ManagersName,ManagersPsw,CreateTime,ModifyTime,RoleId,RoleName) VALUES (:ManagersName,:ManagersPsw,:ModifyTime,:CreateTime,:RoleId,:RoleName)";
                managers.CreateTime = System.DateTime.Now;
                managers.ModifyTime= System.DateTime.Now;
                var Collectlist = new { ManagersName = managers.ManagersName,ManagersPsw=managers.ManagersPsw,CreateTime = managers.CreateTime, ModifyTime=managers.ModifyTime, RoleId = managers.RoleId, RoleName = managers.RoleName };
                int result = conn.Execute(executeSql, Collectlist);
                if (result > 0)
                {
                    string sql = @"select Id from Managers where ManagersName=:ManagersName";
                    var a = new { ManagersName = managers.ManagersName };
                    var Id = conn.Query(sql, a).FirstOrDefault();
                    var PowerId = managers.RoleId.Split(',');
                    for (int i = 0; i < PowerId.Length; i++)
                    {
                        UserRole roleAction = new UserRole();
                        roleAction.UserId = int.Parse(Id.Values.FirstOrDefault().ToString());
                        roleAction.RoleId = Convert.ToInt32(PowerId[i]);
                        roleAction.CreateTime = System.DateTime.Now;
                        roleAction.ModifyTime = System.DateTime.Now;
                        string sql1 = @"insert into UserRole (UserId,RoleId,CreateTime,ModifyTime) VALUES (:UserId,:RoleId,:CreateTime,:ModifyTime)";
                        int result1 = conn.Execute(sql1, roleAction);
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteManagers(int Id)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string executeSql = @"delete from Managers where Id=:Id";
                var Collectlist = new { Id = Id };
                int result = conn.Execute(executeSql, Collectlist);
                if (result > 0)
                {
                    string executeSqls = @"delete from UserRole where UserId=:Id";
                    var Collectlists = new { Id = Id };
                    int results = conn.Execute(executeSqls, Collectlists);
                }
                return result;
            }
        }

        /// <summary>
        /// 后台用户显示
        /// </summary>
        /// <returns></returns>
        public List<Managers> GetManagers()
        {

            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select Id,ManagersName,CreateTime,ModifyTime,RoleName from Managers";
                var result = conn.Query<Managers>(sql, null);
                return result.ToList();

            }

        }

        public List<Managers> GetManagersById(int Id)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select * from Managers where Id=:Id";
                var Collectlist = new { Id = Id };
                var result = conn.Query<Managers>(sql, Collectlist);
                return result.ToList();
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="ManagersName"></param>
        /// <param name="ManagersPsw"></param>
        /// <returns></returns>
        public Managers Login(string ManagersName, string ManagersPsw)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = @"select Id,ManagersName,ManagersPsw from Managers where ManagersName=:ManagersName and ManagersPsw=:ManagersPsw";
                var conditon = new { ManagersName = ManagersName, ManagersPsw = ManagersPsw };
                var managers = conn.Query<Managers>(sql, conditon).FirstOrDefault();
                if (managers != null)
                {
                    string sql2 = @"select * from power where id in(select  powerid  from roleaction where roleid in(select roleid from userrole where userid=(select id from managers where managersname=:ManagersName and managerspsw=:ManagersPsw)))";
                    var conditon2 = new { ManagersName = ManagersName, ManagersPsw = ManagersPsw };
                    var result2 = conn.Query<Power>(sql2, conditon2);
                    managers.PowerList = result2.ToList();
                    return managers;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="managers"></param>
        /// <returns></returns>
        public int UpdateManagers(Managers managers)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string executeSql = @" Update Managers set ManagersName=:ManagersName,ManagersPsw=:ManagersPsw,RoleId=:RoleId,RoleName=:RoleName,CreateTime=:CreateTime,ModifyTime=:ModifyTime where Id=:Id";
                managers.CreateTime = System.DateTime.Now;
                managers.ModifyTime = System.DateTime.Now;
                var Collectlist = new { ManagersName = managers.ManagersName, ManagersPsw=managers.ManagersPsw,CreateTime =managers.CreateTime, RoleId = managers.RoleId, RoleName = managers.RoleName, ModifyTime = managers.ModifyTime, Id = managers.Id };
                int result = conn.Execute(executeSql, Collectlist);
                if (result > 0)
                {
                    string sql = @"select Id from Managers where ManagersName=:ManagersName";
                    var a = new { ManagersName = managers.ManagersName };
                    var Ids = conn.Query(sql, a).FirstOrDefault();
                    string executeSqls = @"delete from UserRole where UserId=:Id";
                    var Collectlists = new { Id = managers.Id };
                    int results = conn.Execute(executeSqls, Collectlists);
                    var PowerIds = managers.RoleId.Split(',');
                    for (int i = 0; i < PowerIds.Length; i++)
                    {
                        UserRole roleAction = new UserRole();
                        roleAction.UserId = int.Parse(Ids.Values.FirstOrDefault().ToString());
                        roleAction.RoleId = Convert.ToInt32(PowerIds[i]);
                        roleAction.CreateTime= System.DateTime.Now;
                        roleAction.ModifyTime = System.DateTime.Now;
                        string sql1 = @"insert into UserRole (RoleId,UserId,CreateTime,ModifyTime) VALUES (:RoleId,:UserId,:CreateTime,:ModifyTime)";
                        int result1 = conn.Execute(sql1, roleAction);
                    }
                }
                return result;
            }
        }
    }
}
