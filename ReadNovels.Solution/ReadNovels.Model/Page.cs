using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadNovels.Model
{
   public class Page
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int Currentpage { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int Totalpage { get; set; }
    }
}
