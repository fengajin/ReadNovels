using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.IService
{
    using ReadNovels.Model;
    /// <summary>
    /// 更新小说--接口
    /// </summary>
    public interface ICaptureUpdateService
    {

        /// <summary>
        /// 通过多条件进行查询
        /// </summary>
        /// <returns></returns>
        List<Capture> GetCaptureByMore();



    }
}
