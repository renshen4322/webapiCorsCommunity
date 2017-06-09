using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model
{
   public class SelectProjectImageModel
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 项目名
        /// </summary>
        public string project_name { get; set; }
        /// <summary>
        /// 图片名
        /// </summary>
        public string image_name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string image_uri { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime created_date { get; set; }
    }
}
