using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReadNovels.WebApi.Controllers
{
    using ReadNovels.Model;
    using ReadNovels.Service;
    using ReadNovels.IService;
    using Newtonsoft.Json;
    using ReadNovels.Cache;
    
    [RoutePrefix("wx_UserAPI")]
    public class WX_UserAPIController : ApiController
    {
        WX_UserDAL wx_userdal = new WX_UserDAL();
        IWX_UserDAL _wx_userDal = null;
        public WX_UserAPIController(IWX_UserDAL wx_userDal)
        {
            _wx_userDal = wx_userDal;
        }
      
        /// <summary>
        /// 手机端登录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("login")]
        public WX_User Login(string code)
        {
           
                WX_User clientinfo = new WX_User();
                HttpClient httpclient = new HttpClient();
                string appid = "wx44f49aee4d264dbf";
                string secret = "bdca2ab935f5a99f7bc02be82940741e";

                httpclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = httpclient.PostAsync("https://api.weixin.qq.com/sns/jscode2session?appid=" + appid + "&secret=" + secret + "&js_code=" + code.ToString() + "&grant_type=authorization_code", null).Result;
                var result = "";
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                }
                httpclient.Dispose();
                var results = JsonConvert.DeserializeObject<WX_User>(result);
                clientinfo.Openid = results.Openid;
                clientinfo.Session_key = results.Session_key;//密钥
                var client = _wx_userDal.WX_UserShow(clientinfo.Openid);//判断是否为已注册用户
                if (client == null)
                {
                  client= _wx_userDal.WX_UserAdd(clientinfo);
                
                }
                RedisHelper.Set<WX_User>(clientinfo.Session_key, clientinfo, DateTime.Now.AddMinutes(10));
                return client;
        }
       
    }
}
