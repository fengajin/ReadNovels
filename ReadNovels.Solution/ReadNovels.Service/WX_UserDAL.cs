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

    public class WX_UserDAL : IWX_UserDAL
    {
        /// <summary>
        /// 微信用户添加
        /// </summary>
        /// <param name="wx_user"></param>
        /// <returns></returns>
        public WX_User  WX_UserAdd(WX_User wx_user)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string executeSql = @" INSERT INTO WX_USER (openid, Session_key, username, userbirthday, usersex, createtime, modifytime) VALUES (:openid, :Session_key, :username, :userbirthday, :usersex, :createtime, :modifytime) ";
                int result=  conn.Execute(executeSql, wx_user);
                if (result > 0)
                {
                    string sql2 = "select  id, openid, session_key from wx_user where openid=:openid";
                    var param = new { openid = wx_user.Openid };
                    var wx_User = conn.Query<WX_User>(sql2, param).SingleOrDefault();
                    return wx_User;
                }
                else
                {
                    return null;
                }

                
            }

        }
        /// <summary>
        /// 微信用户显示
        /// </summary>
        /// <returns></returns>
        public WX_User WX_UserShow(string openid)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string sql = "select * from wx_user where Openid=:Openid";
                var param = new { Openid = openid };
                var wx_userlist = conn.Query<WX_User>(sql, param);
                return wx_userlist.ToList().LastOrDefault();
            }
        }
        

    }
}
