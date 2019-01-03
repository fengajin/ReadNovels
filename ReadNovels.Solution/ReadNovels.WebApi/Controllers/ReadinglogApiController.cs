using ReadNovels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReadNovels.WebApi.Controllers
{
    using ReadNovels.IService;

    /// <summary>
    /// 阅读记录分析
    /// </summary>
    [RoutePrefix("readinglogApi")]
    public class ReadinglogApiController : ApiController
    {
        IReadinglogService _readinglogService = null;
        public ReadinglogApiController(IReadinglogService readinglogService)
        {
            _readinglogService = readinglogService;
        }

        /// <summary>
        /// 获取指定时间段内的阅读量排前10的小说
        /// </summary>
        /// <param name="datetime1"></param>
        /// <param name="datetime2"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getReadinglogs")]
        public List<Readinglog> GetReadinglogs(DateTime?  datetime1, DateTime?  datetime2)
        {
            //当两个时间都为空时，获取当前时间的月份的第一天，和最后一天
            if (datetime1 == null)
            {
                //获取当前时间的月份的第一天
                datetime1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            if (datetime2 == null)
            {
                //获取当前时间的月份最后一天
                int day = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                datetime2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, day);
            }
            var readingloglist=  _readinglogService.GetReadinglogs(datetime1, datetime2);
            return readingloglist;
        }

        /// <summary>
        /// 进入小说详情页时加入阅读记录表
        /// </summary>
        /// <param name="Novelid"></param>
        /// <param name="Userid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getReadinglogsById")]
        public int GetReadinglogsById(int Novelid, int Userid)
        {
            var result = _readinglogService.GetReadinglogsById(Novelid, Userid);
            return result;
        }
    }
}
