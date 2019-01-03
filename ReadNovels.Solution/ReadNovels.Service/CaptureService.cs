using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.Service
{
    using ReadNovels.IService;
    using ReadNovels.Model;
    using ReadNovels.Common;
    using Dapper;
    using Oracle.DataAccess.Client;

    /// <summary>
    /// 小说抓取Service
    /// </summary>
    public class CaptureService : ICaptureService
    {
        /// <summary>
        /// 添加抓取小说表数据
        /// </summary>
        /// <param name="capture"></param>
        /// <returns></returns>
        public int CaptureAdd(Capture capture)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string executeSql = @" INSERT INTO Capture ( NovelName, Author, Nums, Path, ImgPath, Graburl, URL, WebsiteURL, CreateCapture, ISCapture, ISEnabled, Isverify, Intro, NoveState, Typed) VALUES ( :NovelName, :Author, :Nums, :Path, :ImgPath, :Graburl, :URL, :WebsiteURL, :CreateCapture, :ISCapture, :ISEnabled, :Isverify, :Intro, :NoveState, :Typed)";
                return conn.Execute(executeSql, capture);
            }
        }

        /// <summary>
        /// 返回抓取小说表Id
        /// </summary>
        /// <param name="NovelName">小说名称</param>
        /// <param name="Author">小说作者</param>
        /// <returns></returns>
        List<Capture> ICaptureService.GetCaptures(string NovelName, string Author)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string captureByIdSql = @" select Id from Capture where NovelName = :NovelName and Author = :Author ";
                var conditon = new { NovelName = NovelName, Author = Author };
                var result = conn.Query<Capture>(captureByIdSql, conditon);
                return result.ToList();
            }
        }

        /// <summary>
        /// 返回抓取小说表Id(无接口)
        /// </summary>
        /// <param name="NovelName">小说名称</param>
        /// <param name="Author">小说作者</param>
        /// <returns></returns>
        public List<Capture> GetCapturesCount(string NovelName, string Author)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string captureByIdSql = @" select Id from Capture where NovelName = :NovelName and Author = :Author ";
                var conditon = new { NovelName = NovelName, Author = Author };
                var result = conn.Query<Capture>(captureByIdSql, conditon);
                return result.ToList();
            }
        }

        /// <summary>
        /// 添加抓取章节表数据
        /// </summary>
        /// <param name="captureSection"></param>
        /// <returns></returns>
        public int CaptureSectionAdd(CaptureSection captureSection)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string executeSql = @" INSERT INTO CaptureSection ( Captureid, ChapterName, ChapterURL, CaptureTime, ChapterNum ) VALUES ( :Captureid, :ChapterName, :ChapterURL, :CaptureTime, :ChapterNum)";
                var result = conn.Execute(executeSql, captureSection);
                return result;
            }
        }

        /// <summary>
        /// 获取表中是否是存在此类别
        /// </summary>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        public List<Types> GetTypes(string TypeName)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string captureByIdSql = @" select Id from Types where TypeName = :TypeName";
                var conditon = new { TypeName = TypeName };
                var result = conn.Query<Types>(captureByIdSql, conditon);
                return result.ToList();
            }
        }

        /// <summary>
        /// 添加抓取的类别数据
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public int TypesAdd(Types types)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string executeSql = @" INSERT INTO Types ( TypeName, CreateTime, ModifyTime ) VALUES ( :TypeName, :CreateTime, :ModifyTime)";
                return conn.Execute(executeSql, types);
            }
        }

        /// <summary>
        /// 判断此章节是否被抓取
        /// </summary>
        /// <param name="CaptureId">小说Id</param>
        /// <param name="ChapterName">章节名称</param>
        /// <returns></returns>
        public List<CaptureSection> GetCaptureSection(int CaptureId, string ChapterName)
        {
            using (OracleConnection conn = DapperHelper.GetConnString())
            {
                string captureByIdSql = @"select Id from CAPTURESECTION where captureid = :CaptureId and chaptername = :ChapterName";
                var conditon = new { CaptureId = CaptureId, ChapterName = ChapterName };
                var result = conn.Query<CaptureSection>(captureByIdSql, conditon);
                return result.ToList();
            }
        }
        
    }
}
