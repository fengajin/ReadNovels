using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

//引用命名空间
using System.Web.Script.Serialization;
using System.Web.Security;
using Utility;
using Utilty;
using ReadNovels.Model;
using NLog;

namespace ReadNovels.MVC.Controllers
{
    public class BaseController : Controller
    {
        private Managers _loginInfo;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public Managers LoginInfo
        {
            get
            {
                //当前请求的用户身份是否合法
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var strUserData = ((FormsIdentity)User.Identity).Ticket.UserData;
                    _loginInfo = new JavaScriptSerializer().Deserialize<Managers>(strUserData);

                    _loginInfo = RedisHelper.Get<Managers>(_loginInfo.Id.ToString());
                }
                return _loginInfo;
            }
        }

        /// <summary>
        /// 用户登录成功后，将用户信息缓存起来
        /// </summary>
        /// <param name="userData"></param>
        public static void WriteDataToCookie(Managers userData)
        {
            //将实体对象序列化为json字符串
            var strUserData = new JavaScriptSerializer().Serialize(userData);

            //将json字符串生成对应的令牌数据  persistent 持久化
            var ticket = new FormsAuthenticationTicket(1, userData.Id.ToString(), DateTime.Now, DateTime.Now.AddHours(12), false, strUserData);
            var ticketVal = FormsAuthentication.Encrypt(ticket);

            CookiesHelper.SetCookie(FormsAuthentication.FormsCookieName, ticketVal, ticket.Expiration);

            RedisHelper.Set<Managers>(userData.Id.ToString(), userData);
        }

        /// <summary>
        /// 动作过滤器-动作执行之前的事件
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.LoginInfo = LoginInfo;

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 异常过滤器
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                base.OnException(null);
                return;
            }

            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();
            var timeStamp = filterContext.HttpContext.Timestamp;

            var parmId = string.Empty;
            if (filterContext.RouteData.Values["id"] != null)
            {
                parmId = filterContext.RouteData.Values["id"].ToString();
            }

            //记录异常信息到 /common/Log.txt
            var message = new StringBuilder();
            message.Append(Environment.NewLine);
            message.Append("请求信息：");
            message.Append(Environment.NewLine);
            message.AppendFormat("Controller={0}，Action={1}，ParmId={2}，TimeStamp={3}", controllerName, actionName, parmId, timeStamp);
            message.Append(Environment.NewLine);
            message.Append("异常信息：");
            message.Append(Environment.NewLine);
            message.Append(filterContext.Exception);

            logger.Debug(message.ToString());
        }
    }

    /// <summary>
    /// 授权过滤器
    /// </summary>
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 授权过滤方法
        /// </summary>
        /// <param name="filterContext">授权上下文</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //已认证用户
                var strUserData = ((FormsIdentity)filterContext.HttpContext.User.Identity).Ticket.UserData;
                var _loginInfo = new JavaScriptSerializer().Deserialize<Managers>(strUserData);
                _loginInfo = RedisHelper.Get<Managers>(_loginInfo.Id.ToString());        

                var action = filterContext.RouteData.Values["Action"];
                var controller = filterContext.RouteData.Values["Controller"];
                var tmpVal = (controller + "/" + action).ToLower();

                //filterContext.HttpContext.Response.Redirect("/student/index");
            }
            else
            {
                 FormsAuthentication.SignOut();
            }

            // if (filterContext.HttpContext.Session["UserInfo"] == null)
            // {
            //      filterContext.HttpContext.Response.Redirect("/home/login");
            //  }
        }
    }
}