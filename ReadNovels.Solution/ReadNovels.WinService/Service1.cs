using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

using System.IO;
using ReadNovels.Capture;
using ReadNovels.WinService.Model;
using ReadNovels.WinService.Commom;

namespace CaptureWindowsService
{
    /// <summary>
    /// timer定时器-抓取
    /// </summary>
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            //设置timer调取时间
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimedEvent);
            timer.Interval = 60000;//每60秒执行一次
            timer.Enabled = true;
        }
        //全局变量
        public int count = 0;

        private void TimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                //抓取更新小说-公共类库
                CaptureUpdate_Novel captureUpdate_Novel = new CaptureUpdate_Novel();
                //如果当前时间是18点30分
                if (DateTime.Now.Hour == 18 && DateTime.Now.Minute == 30)
                {
                    //业务逻辑代码
                    GradNovelClass gradNovelClass = new GradNovelClass();
                    var number = gradNovelClass.GetCaptures();
                    //循环多次获取
                    //获取URL
                    for (int i = 0; i < number; i++)
                    {
                        //获取获取小说抓取表中的所有URL
                        var list = gradNovelClass.GetCapturesURL();

                        int Id = list[i].Id;//Id
                        string NovelName = list[i].NovelName;//小说名称
                        string Author = list[i].Author;//小说作者
                        string Graburl = list[i].Graburl;//抓取详情Url
                        string URL = list[i].URL;//所在网站
                        string WebsiteURL = list[i].WebsiteURL;//抓取内容Url
                        string Typed = list[i].Typed;//抓取类别

                        //调用更新类
                        //captureUpdate_Novel.GrabNovelInfo(Graburl, WebsiteURL);

                        //写入记录
                        //string strMsg = "\n\n抓取id：" + Id.ToString() + "\r抓取小说名称：" + NovelName + "\r抓取作者：" + Author + "\r抓取详情Url：" + Graburl + "\r抓取网站：" + URL + "\r抓取内容Url：" + WebsiteURL + "\r抓取类型：" + Typed + "\n\n";

                        string strMsg = "\r\n第：" + (i + 1).ToString() + " 本书" + "\r抓取小说名称：" + NovelName + "，\r抓取作者：" + Author + "，\r抓取网站：" + URL + "，\r抓取类型：" + Typed + "；\n\n";
                        this.WriteLog(strMsg);//调用
                    }
                    //记录更新量
                    this.WriteLog("\n本次共更新：" + number.ToString() + "本书籍");
                }
            }
            catch (Exception ex)
            {
                //写入错误信息
                this.ErrorWriteLog(ex.Message);
            }
        }

        //启动服务
        protected override void OnStart(string[] args)
        {
            this.WriteLog("\n\n当前时间：" + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + "\n");
            this.WriteLog("客户端书籍抓取更新服务：【服务启动】");
        }

        //停止服务
        protected override void OnStop()
        {
            this.WriteLog("\n当前时间：" + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + "\n");
            this.WriteLog("客户端书籍抓取更新服务：【服务停止】");
        }

        //关闭计算机
        protected override void OnShutdown()
        {
            this.WriteLog("\n当前时间：" + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + "\n");
            this.WriteLog("客户端书籍抓取更新服务：【计算机关闭】");
        }

        #region 记录日志

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="msg"></param>
        private void WriteLog(string msg)
        {
            //string strpath = @"D;\Log.txt";
            //改日志文件会存在windows服务程序目录下
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\log.txt";
            FileInfo fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
            {
                FileStream fileStream;
                fileStream = File.Create(path);
                fileStream.Close();
            }

            //写入日志
            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fs))
                {
                    streamWriter.WriteLine(DateTime.Now.ToString() + "   " + msg);
                }
            }
        }

        /// <summary>
        /// 错误记录日志
        /// </summary>
        /// <param name="msg"></param>
        private void ErrorWriteLog(string msg)
        {
            //string strpath = @"D;\ErrorLog.txt";
            //改日志文件会存在windows服务程序目录下
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\ErrorLog.txt";
            FileInfo fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
            {
                FileStream fileStream;
                fileStream = File.Create(path);
                fileStream.Close();
            }

            //写入日志
            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fs))
                {
                    streamWriter.WriteLine(DateTime.Now.ToString() + "   " + msg);
                }
            }
        }

        #endregion

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

        }
    }
}
