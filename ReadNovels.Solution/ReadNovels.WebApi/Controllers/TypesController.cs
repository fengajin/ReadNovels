using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ReadNovels.IService;


using ReadNovels.Model;

using ReadNovels.Service;

namespace ReadNovels.WebApi.Controllers
{
    /// <summary>
    /// 获取分类WebApi
    /// </summary>
    [RoutePrefix("types")]
    public class TypesController : ApiController
    {
        //ITypeServices _typeServices = null;
        //public TypesController(ITypeServices typeServices)
        //{
        //    _typeServices = typeServices;
        //}

        /// <summary>
        /// 获取分类
        /// </summary>
        [HttpGet]
        [Route("getTypes")]
        public List<Types> GetTypes()
        {
            var result = new TypeService().GetTypes();
            return result;
        }
    }
}