using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Image
{
    /// <summary>
    /// 获取系统使用的图片列表
    /// </summary>
   public class GetSystemImageList:QueryRequestDto
    {
       /// <summary>
       /// 项目id
       /// </summary>
       public int? project_id { get; set; }
    
    }
   public class GetSystemImageListResponse : QueryResponseDto
   {
       public GetSystemImageListResponse()
       {
           data=new List<SystemImageDto>();
       }

       public List<SystemImageDto> data { get; set; }
   }

    public class SystemImageDto
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
        public long created_at { get; set; }
    }
}
