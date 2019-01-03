using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dapper;
using ReadNovels.WinService.Model;
using Oracle.DataAccess.Client;

namespace ReadNovels.WinService.Commom
{
    public class GradNovelClass
    {
        /// <summary>
        /// 获取小说抓取表中的所有URL
        /// </summary>
        /// <returns></returns>
        public List<ReadNovels.WinService.Model.Capture> GetCapturesURL()
        {
            using (OracleConnection connection = DapperHelper.GetConnString())
            {
                string strSql = @"select Id,NovelName,Author,Graburl,URL,WebsiteURL,Typed from Capture ";
                var result = connection.Query<ReadNovels.WinService.Model.Capture>(strSql, null);
                return result.ToList();
            }
        }
        
        /// <summary>
        /// 返回抓取小说表Id.Count()
        /// </summary>
        /// <returns></returns>
        public int GetCaptures()
        {
            using (OracleConnection connection = DapperHelper.GetConnString())
            {
                string captureByIdSql = @" select Id from Capture";
                var result = connection.Query<ReadNovels.WinService.Model.Capture>(captureByIdSql, null).Count();
                return result;
            }
        }



    }
}
