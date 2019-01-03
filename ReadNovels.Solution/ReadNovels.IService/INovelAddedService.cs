using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.IService
{
    using ReadNovels.Model;
   public interface INovelAddedService
    {
        List<Novel> NovelAdded();
        int Uptdate(int Id, int IFShelf);
    }
}
