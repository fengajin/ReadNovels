using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReadNovels.WebApi.Controllers
{
    using ReadNovels.Model;
    using ReadNovels.IService;

    [RoutePrefix("role")]
    public class RoleController : ApiController
    {
        private const int PageCount = 3;

        IRoleService iroleService = null;
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="roleService"></param>
        public RoleController(IRoleService roleService)
        {
            iroleService = roleService;
        }

        [HttpPost]
        [Route("addRole")]
        public int AddRole([FromBody]Role r)
        {
            var result = iroleService.AddRole(r);
            return result;
        }

        [HttpGet]
        [Route("getRoles")]
        public List<Role> GetRoles()
        {
            var result = iroleService.GetRole();
            return result;
        }

        [HttpGet]
        [Route("getRole")]
        public Page GetRole(string RoleName,int Page=1)
        {
            var result = iroleService.GetRole();
            Page p = new Page();
            if (!string.IsNullOrWhiteSpace(RoleName))
            {
                result = result.Where(r => r.RoleName.Contains(RoleName)).ToList();
            }
            p.Currentpage = Page;
            p.Totalpage = (result.Count / PageCount) + (result.Count % PageCount == 0 ? 0 : 1);
            p.Data = result.Skip((Page - 1) * PageCount).Take(PageCount);
            return p;
        }

        [HttpGet]
        [Route("deleteRole")]
        public int DeleteRole(int Id)
        {
            var result = iroleService.DeleteRole(Id);
            return result;
        }

        [HttpGet]
        [Route("getRoleById")]
        public List<Role> GetRoleById(int Id)
        {
            var result = iroleService.GetRoleById(Id);
            return result;
        }

        [HttpPost]
        [Route("updateRole")]
        public int UpdateRole(Role role)
        {
            var result = iroleService.UpdateRole(role);
            return result;
        }
    }
}
