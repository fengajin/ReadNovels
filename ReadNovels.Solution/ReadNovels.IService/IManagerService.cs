using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadNovels.Model;

namespace ReadNovels.IService
{
    public interface IManagerService
    {
        /// <summary>
        /// 后台用户显示
        /// </summary>
        /// <returns></returns>
        List<Managers> GetManagers();
        /// <summary>
        /// 后台用户登录
        /// </summary>
        /// <param name="ManagersName"></param>
        /// <param name="ManagersPsw"></param>
        /// <returns></returns>
        Managers Login(string ManagersName, string ManagersPsw);
        int AddManagers(Managers managers);
        int DeleteManagers(int Id);
        List<Managers> GetManagersById(int Id);
        int UpdateManagers(Managers managers);
    }
}
