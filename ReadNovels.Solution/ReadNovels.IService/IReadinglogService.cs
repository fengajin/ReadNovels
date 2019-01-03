using ReadNovels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.IService
{
   public interface IReadinglogService
    {
        /// <summary>
        /// 获取指定时间段内的阅读量排前10的小说
        /// </summary>
        /// <param name="datetime1"></param>
        /// <param name="datetime2"></param>
        /// <returns></returns>
         List<Readinglog> GetReadinglogs(DateTime? datetime1, DateTime? datetime2);
        /// <summary>
        /// 进入小说详情页时加入阅读记录表
        /// </summary>
        /// <param name="Novelid"></param>
        /// <param name="Userid"></param>
        /// <returns></returns>
        int GetReadinglogsById(int Novelid, int Userid);
    }
}
