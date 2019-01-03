using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.IService
{
    using ReadNovels.Model;
    
    public interface INovelsService
    {
        List<Novel> GetNovelsMan();
        List<Novel> GetNovelsMen();
        List<Novel> GetNovelsAll();
        List<Novel> GetNovelsManMore();
        List<Novel> GetNovelsMenMore();
        List<Novel> SearchPages(string NovelName);
    }
}
