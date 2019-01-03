using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.Model
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 主键Id
        /// </summary>
       public int Id { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
       public string RoleName { get; set; }
        /// <summary>
        /// 权限ids
        /// </summary>
       public string PowerId { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
       public string PowerName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
       public DateTime? ModifyTime { get; set; }
    }
}
