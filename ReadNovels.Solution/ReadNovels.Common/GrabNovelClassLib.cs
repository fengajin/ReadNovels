using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace ReadNovels.Common
{
    /// <summary>
    /// 抓取小说信息类库
    /// </summary>
    public class GrabNovelClassLib
    {

        /// <summary>
        /// 最新抓取网站
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetHtmlCode(string url)
        {
            string htmlCode;
            HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            webRequest.Timeout = 30000;
            webRequest.Method = "GET";
            webRequest.UserAgent = "Mozilla/4.0";
            webRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();
            if (webResponse.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压
            {
                using (System.IO.Stream streamReceive = webResponse.GetResponseStream())
                {
                    using (var zipStream =
                        new System.IO.Compression.GZipStream(streamReceive, System.IO.Compression.CompressionMode.Decompress))
                    {
                        Encoding enc = GetEncoding(url);
                        using (StreamReader sr = new System.IO.StreamReader(zipStream, enc))
                        {
                            htmlCode = sr.ReadToEnd();
                        }
                    }
                }
            }
            else
            {
                using (System.IO.Stream streamReceive = webResponse.GetResponseStream())
                {
                    Encoding enc = GetEncoding(url);
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(streamReceive, enc))
                    {
                        htmlCode = sr.ReadToEnd();
                    }
                }
            }
            return htmlCode;
        }
        public Encoding GetEncoding(string strurl)
        {
            string urlToCrawl = strurl;
            //generate http request
            if (urlToCrawl != null && urlToCrawl != "")
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlToCrawl);
                //use GET method to get url's html
                req.Method = "GET";
                req.Accept = "*/*";
                req.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                req.ContentType = "text/xml";
                //use request to get response
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Encoding enc;
                try
                {
                    if (resp.CharacterSet != "ISO-8859-1")
                        enc = Encoding.GetEncoding(resp.CharacterSet);
                    else
                        enc = Encoding.UTF8;
                }
                catch
                {
                    // *** Invalid encoding passed
                    enc = Encoding.UTF8;
                }
                string sHTML = string.Empty;
                using (StreamReader read = new StreamReader(resp.GetResponseStream(), enc))
                {
                    sHTML = read.ReadToEnd();
                    Match charSetMatch = Regex.Match(sHTML, "charset=(?<code>[a-zA-Z0-9\\-]+)", RegexOptions.IgnoreCase);
                    string sChartSet = charSetMatch.Groups["code"].Value;
                    //if it's not utf-8,we should redecode the html.
                    if (!string.IsNullOrEmpty(sChartSet) && !sChartSet.Equals("utf8", StringComparison.OrdinalIgnoreCase))
                    {
                        enc = Encoding.GetEncoding(sChartSet);
                    }
                }
                return enc;
            }
            return Encoding.Default;
        }

        #region 废弃的抓取HTML方法
        /*
        /// <summary>
        /// 获取Html页详情文本
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns></returns>
        public string HttpGetParticulars(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            HttpWebResponse response;
            request.ContentType = "text/html; charset=utf8";
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("UTF-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        /// <summary>
        /// 获取Html页内容文本
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns></returns>
        public string HttpGetComtent(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            HttpWebResponse response;
            request.ContentType = "text/html; charset=utf8";
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("UTF-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        /// <summary>
        /// 获取Html页章节内容文本
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns></returns>
        public string HttpGetSection(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            HttpWebResponse response;
            request.ContentType = "text/html; charset=utf8";
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("UTF-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        ///// <summary>
        ///// 创建文本
        ///// </summary>
        ///// <param name="content">内容</param>
        ///// <param name="name">名字</param>
        ///// <param name="path">路径</param>
        //public void CreateNovel(string content, string name, string path)
        //{
        //    string Log = content;// + "\r\n";
        //    // 创建文件夹，如果不存在就创建file文件夹
        //    if (Directory.Exists(path) == false)
        //    {
        //        Directory.CreateDirectory(path);
        //    }
        //    // 判断文件是否存在，不存在则创建
        //    if (!System.IO.File.Exists(path + name + ".txt"))
        //    {
        //        FileStream fs1 = new FileStream(path + name + ".txt", FileMode.Create, FileAccess.Write);// 创建写入文件 
        //        StreamWriter sw = new StreamWriter(fs1);
        //        sw.WriteLine(Log);// 开始写入值
        //        sw.Close();
        //        fs1.Close();
        //    }
        //    else
        //    {
        //        FileStream fs = new FileStream(path + name + ".txt" + "", FileMode.Append, FileAccess.Write);
        //        StreamWriter sr = new StreamWriter(fs);
        //        sr.WriteLine(Log);// 开始写入值
        //        sr.Close();
        //        fs.Close();
        //    }
        //}
        

        public string HttpPost(string Url, string postDataStr)
        {
            CookieContainer cookie = new CookieContainer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            request.CookieContainer = cookie;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            response.Cookies = cookie.GetCookies(response.ResponseUri);

            Stream myResponseStream = response.GetResponseStream();

            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        */
        #endregion
    }
}
