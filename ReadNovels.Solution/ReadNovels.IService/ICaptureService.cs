using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.IService
{
    using ReadNovels.Model;
    /// <summary>
    /// 小说抓取、保存接口
    /// </summary>
    public interface ICaptureService
    {
        /// <summary>
        /// 小说抓取添加
        /// </summary>
        /// <param name="capture">抓取小说表</param>
        /// <returns></returns>
        int CaptureAdd(Capture capture);

        /// <summary>
        /// 判断小说是否已抓取
        /// </summary>
        /// <param name="NovelName">小说名</param>
        /// <param name="Author">小说作者</param>
        /// <returns></returns>
        List<Capture> GetCaptures(string NovelName, string Author);

        /// <summary>
        /// 判断此章节是否被抓取
        /// </summary>
        /// <param name="CaptureId">小说Id</param>
        /// <param name="ChapterName">章节名称</param>
        /// <returns></returns>
        List<CaptureSection> GetCaptureSection(int CaptureId, string ChapterName);

        /// <summary>
        /// 小说抓取章节添加
        /// </summary>
        /// <param name="captureSection">抓取小说章节</param>
        /// <returns></returns>
        int CaptureSectionAdd(CaptureSection captureSection);

        /// <summary>
        /// 获取表中是否是存在此类别
        /// </summary>
        /// <param name="TypeName">类别名称</param>
        /// <returns></returns>
        List<Types> GetTypes(string TypeName);

        /// <summary>
        /// 类别添加
        /// </summary>
        /// <param name="type">类别</param>
        /// <returns></returns>
        int TypesAdd(Types types);

    }
}
