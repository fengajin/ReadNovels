using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.Model
{
    /// <summary>
    /// 用户角色关联表
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// 主键Id
        /// </summary>
       public int Id { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
       public int RoleId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
       public int UserId { get; set; }
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
