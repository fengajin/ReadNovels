using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




using ReadNovels.Model;


namespace ReadNovels.IService
{
    public interface ITypeServices
    {
        List<Types> GetTypes();
        
    }
}
