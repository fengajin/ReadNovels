using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.Model
{
    /// <summary>
    /// 后台用户表
    /// </summary>
    public  class Managers
    {
        /// <summary>
        /// 管理员Id
        /// </summary>
       public int Id { get; set; }
        /// <summary>
        /// 管理员名称
        /// </summary>
       public string ManagersName { get; set; }
        /// <summary>
        /// 管理员密码
        /// </summary>
       public string ManagersPsw { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
       public string ManagersNum { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
       public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
       public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 角色Ids
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 权限集合
        /// </summary>
        public List<Power> PowerList { get; set; }
    }
}
