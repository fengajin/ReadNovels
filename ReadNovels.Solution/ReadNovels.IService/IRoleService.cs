using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.IService
{
    using Model;
    public interface IRoleService
    {
        List<Role> GetRole();
        int AddRole(Role r);
        int DeleteRole(int Id);
        List<Role> GetRoleById(int Id);
        int UpdateRole(Role role);
    }
}
