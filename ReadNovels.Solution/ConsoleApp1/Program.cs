using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using System.Text.RegularExpressions;
using ReadNovels.Common;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //共33637本
                for (int i = 1; i <= 100000; i++)
                {
                    //循环得到url
                    string strUrl = "https://www.23us.so/xiaoshuo/" + i + ".html";

                    //实例化"获取Html页文本"对象
                    GrabNovelClassLib grabNovelClassLib = new GrabNovelClassLib();// 顶点抓取小说网站小说小说封面、名称、作者、小说状态、小说字数、内容简介
                    //获取小说详情的Url

                    //int j = 0;
                    //string reg = @"<a[^>]*href=([""'])?(?<href>[^'""]+)\1[^>]*>";
                    //MatchCollection matches = Regex.Matches(novelDetailHtml, reg, RegexOptions.IgnoreCase);

                    //foreach (Match item in matches)
                    //{
                    //    var href = item.Groups["href"].Value;
                    //    j += 1;
                    //    if (j == 44)
                    //    {
                    //        Console.WriteLine(strUrl);
                    //        Console.WriteLine(href);
                    //    }
                    //}

                    Console.WriteLine(strUrl);
                    string novelDetailHtml = grabNovelClassLib.GetHtmlCode(strUrl);
                }
            }
            catch (Exception ex)
            {
                int strex = ex.Message != null && ex.Message != "" ? 1 : 0;
                if (strex == 1)
                {
                    Console.WriteLine("服务器返回404，循环已经到顶，未找到该书籍");
                }
            }
            Console.ReadKey();
        }
    }
}
