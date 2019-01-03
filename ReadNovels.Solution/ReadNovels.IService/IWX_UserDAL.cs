using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.IService
{
    using ReadNovels.Model;
   public interface IWX_UserDAL
    {
        WX_User WX_UserAdd(WX_User wx_user);
        WX_User WX_UserShow(string openid);
    }
}
