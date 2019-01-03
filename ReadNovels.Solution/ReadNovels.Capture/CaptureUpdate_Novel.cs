using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.Capture
{
    using ReadNovels.Model;
    using ReadNovels.IService;
    using ReadNovels.Common;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Text;
    using System.Net;
    using ReadNovels.Service;

    //抓取更新小说-公共类库
    public class CaptureUpdate_Novel
    {
        /// 返回是否执行成功
        /// 0未成功、1成功 3已存在
        int endPrice = 0;

        /// </summary>
        /// 抓取整本小说(按章节创建TXT文件)
        /// </summary>
        /// <param name="getParticularsUrl">获取小说详情的Url</param>
        /// <param name="getContentUrl">获取小说内容的Url</param>
        //[HttpPost]
        //[Route("grabnovel")]
        public int GrabNovelInfo(string getParticularsUrl, string getContentUrl)
        {
            try
            {
                //实例化CaptureService
                CaptureService captureService = new CaptureService();

                //实例化"获取Html页文本"对象
                GrabNovelClassLib grabNovelClassLib = new GrabNovelClassLib();// 顶点抓取小说网站小说小说封面、名称、作者、小说状态、小说字数、内容简介
                //获取小说详情的Url
                string novelDetailHtml = grabNovelClassLib.GetHtmlCode(getParticularsUrl);

                //实例化小说抓取表
                ReadNovels.Model.Capture capture = new ReadNovels.Model.Capture();
                capture.Graburl = getParticularsUrl;
                capture.URL = "顶点小说网";
                capture.WebsiteURL = getContentUrl;
                capture.CreateCapture = DateTime.Now;
                capture.ISCapture = 1;
                capture.ISEnabled = 1;
                capture.Isverify = 0;

                //小说类别表
                Types types = new Types();

                string noveluthor = "";//小说作者

                //抓取小说
                WebClient webClient = new WebClient();
                if (getParticularsUrl != null)
                {
                    // 获取小说名字
                    //定义抓取正则
                    string strReg = "(?<=meta name=\"keywords\" content=\").*?(?=\")";
                    //匹配正则
                    Match ma_name = Regex.Match(novelDetailHtml, strReg);
                    string novelName = ma_name.Groups[0].Value.ToString();//
                    capture.NovelName = novelName;//小说名称

                    // 获取小说封面图片url
                    //正则表达式匹配img的src
                    Regex regexImgSrc = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgSrc>[^\s\t\r\n""'<>]*)[^<>]*?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
                    //搜索匹配的字符串
                    MatchCollection matchCollectionImgSrc = regexImgSrc.Matches(novelDetailHtml);
                    int j = 0;
                    string[] strImgSrcList = new string[matchCollectionImgSrc.Count];
                    //定义保存地址
                    string saveImgName = System.DateTime.Now.ToFileTimeUtc().ToString(); // 128650040772968750
                    string saveImgPath = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + "CoverPicture/" + saveImgName + ".jpg";
                    //取到匹配项列表
                    foreach (Match match in matchCollectionImgSrc)
                    {
                        strImgSrcList[j++] = match.Groups["imgSrc"].Value;
                        if (strImgSrcList[1] != null)
                        {
                            var imgPath = match.Groups["imgSrc"].Value;//正则表达式匹配出的下载地址
                            capture.ImgPath = imgPath;//小说封面路径
                            webClient.DownloadFile(imgPath, saveImgPath);//开始下载封面
                        }
                    }

                    // 获取table中的信息
                    Regex reg_table = new Regex(@"<table cellspacing=""1"" cellpadding=""0"" bgcolor=""#E4E4E4"" id=""at"">(.|\n)*?</table>");
                    var mat_mulu = reg_table.Match(novelDetailHtml);
                    string mulu = mat_mulu.Groups[0].ToString();

                    // 匹配td标签里面的Value
                    var regex = new System.Text.RegularExpressions.Regex(@"<th>(?<item>.*?)</th>[\s\S]+?<td>(?<value>.*?)</td>");
                    var ms = regex.Matches(mulu);
                    foreach (System.Text.RegularExpressions.Match m in ms)
                    {
                        if (m.Groups["item"].Value == "小说类别")
                        {
                            string typed = m.Groups["value"].Value.ToString().Split(';')[1];//小说类别
                            MatchCollection mctypes = Regex.Matches(typed, @"(?isn)<a.+?href=""(?<url>[^""]+)[^>]+>(?<text>.+?)</a>");
                            foreach (Match mtypes in mctypes)
                            {
                                string strtypes = mtypes.Groups["text"].Value;
                                capture.Typed = strtypes;
                                //查询类别表中是否存在此类别
                                var getTypesRow = captureService.GetTypes(strtypes);
                                if (getTypesRow.Count() == 0)//不存在则添加
                                {
                                    types.TypeName = strtypes;
                                    types.CreateTime = DateTime.Now;
                                    types.ModifyTime = DateTime.Now;
                                    int typesBackRow = captureService.TypesAdd(types);//保存抓取的小说类别
                                }
                            }
                        }
                        if (m.Groups["item"].Value == "小说作者")
                        {
                            string author = m.Groups["value"].Value.ToString().Split(';')[1];//小说作者
                            capture.Author = author;//小说作者
                            noveluthor = author;//传出作者
                        }
                        if (m.Groups["item"].Value == "小说状态")
                        {
                            int noveState = Convert.ToInt32(m.Groups["value"].Value.ToString().Split(';')[1] == "完本" ? 0 : 1);//小说状态
                            capture.NoveState = noveState;//小说状态(连载1、完结0)
                        }
                        if (m.Groups["item"].Value == "全文长度")
                        {
                            string nums = m.Groups["value"].Value.ToString().Split(';')[1];//小说全文字数
                            capture.Nums = nums;//小说字数
                            break;
                        }
                    }

                    //小说路径
                    capture.Path = "NovelFile\\" + novelName + "\\";

                    //获取小说内容简介
                    var csQuery = CsQuery.CQ.CreateDocument(novelDetailHtml);
                    string strintro = csQuery["#content dd:eq(3) p:eq(1)"].Text().Trim();//小说内容简介
                    capture.Intro = strintro.Trim();

                    //获检查小说是否存在（小说名称、小说作者）
                    var captureById = captureService.GetCapturesCount(novelName, noveluthor);
                    int existData = captureById.Count;//存在数据
                    if (captureById.Count != 0)
                    {
                        //判断章节是否存在
                        //存在---跳过
                        //调用章节
                        GrabNovelContent(getContentUrl, existData, noveluthor);
                    }
                    else
                    {
                        //不存在小说-则添加
                        //保存抓取小说信息
                        int captureBackRow = captureService.CaptureAdd(capture);
                        endPrice = 1;
                        //调用章节
                        GrabNovelContent(getContentUrl, existData, noveluthor);
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                endPrice = 0;
            }
            return endPrice;
        }

        /// <summary>
        /// 抓取整本小说(按章节创建TXT文件)
        /// </summary>
        /// <param name="getContentUrl">获取小说内容的Url</param>
        /// <param name="existData">小说是否存在</param>
        /// <param name="noveluthor">作者</param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("grabnovelcontent")]
        public int GrabNovelContent(string getContentUrl, int existData, string noveluthor)
        {
            //返回状态(0失败，1成功)
            endPrice = 0;
            try
            {
                //实例化CaptureService
                CaptureService captureService = new CaptureService();
                if (getContentUrl != null)
                {
                    //实例化"获取Html页文本"对象
                    GrabNovelClassLib grabNovelClassLib = new GrabNovelClassLib();
                    //抓取整本小说(按章节创建TXT文件)
                    //获取小说内容的Url
                    string htmlCode = grabNovelClassLib.GetHtmlCode(getContentUrl);

                    // 获取小说名字
                    Match ma_name = Regex.Match(htmlCode, @"<meta name=""keywords"".+content=""(.+)""/>");
                    string novelName = ma_name.Groups[1].Value.ToString().Split(',')[0];

                    //小说章节表
                    CaptureSection captureSection = new CaptureSection();

                    // 获取章节目录
                    Regex reg_mulu = new Regex(@"<table cellspacing=""1"" cellpadding=""0"" bgcolor=""#E4E4E4"" id=""at"">(.|\n)*?</table>");
                    var mat_mulu = reg_mulu.Match(htmlCode);
                    string novelMulu = mat_mulu.Groups[0].ToString();
                    // 匹配a标签里面的url
                    Regex tmpReg = new Regex("<a[^>]+?href=\"([^\"]+)\"[^>]*>([^<]+)</a>", RegexOptions.Compiled);
                    MatchCollection sMC = tmpReg.Matches(novelMulu);
                    //匹配项的数目不为0
                    if (sMC.Count != 0)
                    {
                        //获取小说Id
                        var captureIdList = captureService.GetCapturesCount(novelName, noveluthor);
                        int captureId = captureIdList[0].Id;//小说Id

                        //循环目录url，获取正文内容
                        for (int i = 0; i < sMC.Count; i++)
                        {
                            // 获取章节标题
                            string chapterTitle = sMC[i].Groups[2].Value;

                            //检查此章节是否存在
                            var chapterId = captureService.GetCaptureSection(captureId, chapterTitle);
                            //不存在
                            if (chapterId.Count == 0)
                            {
                                // 获取文章内容
                                string contentsOfArticle = grabNovelClassLib.GetHtmlCode(sMC[i].Groups[1].Value);
                                // 获取正文
                                Regex mainBody = new Regex(@"<dd id=""contents"">(.|\n)*?</dd>");
                                MatchCollection mc = mainBody.Matches(contentsOfArticle);
                                var mat = mainBody.Match(contentsOfArticle);
                                string novelContent1 = mat.Groups[0].ToString().Replace("<dd id=\"contents\">", "").Replace("</dd>", "").Replace("&nbsp;", "").Replace("<br />", "\r\n");
                                string novelContent = Regex.Replace(novelContent1, @"\s\s\s\s\s\s\s\s\s\s", "\r\n");

                                // txt文本输出
                                string savePath = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + "NovelFile/" + novelName + "/";
                                CreateNovel(novelContent, i.ToString(), savePath);//保存本地txt文件

                                captureSection.Captureid = captureId;
                                captureSection.ChapterName = chapterTitle;
                                captureSection.ChapterNum = i;
                                captureSection.ChapterURL = "NovelFile\\" + novelName + "\\" + i + ".txt";//小说路径;
                                captureSection.CaptureTime = DateTime.Now;
                                int captureSectionBackRow = captureService.CaptureSectionAdd(captureSection);//保存抓取小说章节
                                endPrice = 1;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string strEx = ex.Message;
                endPrice = 0;
            }
            return endPrice;
        }

        /// <summary>
        /// 创建文本
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="name">名字</param>
        /// <param name="path">路径</param>
        public void CreateNovel(string content, string name, string path)
        {
            string Log = content;   // + "\r\n";
            // 创建文件夹，如果不存在就创建file文件夹
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            // 判断文件是否存在，不存在则创建
            if (!System.IO.File.Exists(path + name + ".txt"))
            {
                FileStream fileStream = new FileStream(path + name + ".txt", FileMode.Create, FileAccess.Write);// 创建写入文件 
                //FileStream fileStream = new System.IO.FileStream(path + name + ".txt", FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);// 创建写入文件 
                StreamWriter sw = new StreamWriter(fileStream);
                sw.WriteLine(Log);// 开始写入值
                sw.Close();
                fileStream.Close();
            }
            else
            {
                FileStream fileStream = new FileStream(path + name + ".txt" + "", FileMode.Append, FileAccess.Write);
                //FileStream fileStream = new System.IO.FileStream(path + name + ".txt", FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);// 创建写入文件 
                StreamWriter sr = new StreamWriter(fileStream);
                sr.WriteLine(Log);// 开始写入值
                sr.Close();
                fileStream.Close();
            }
        }

    }
}
