using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Image
{
    /// <summary>
    /// 获取现有项目名列表
    /// </summary>
   public class GetImageProjectName:BaseRequestDto
    {

    }
   public class GetImageProjectNameResponse : BaseResponseDto
   {
       public GetImageProjectNameResponse()
       {
           data = new List<Project>();
       }

       /// <summary>
       /// 项目列表
       /// </summary>
       public List<Project> data { get; set; }
   }

    public class Project
    {
        /// <summary>
        /// 项目id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 项目名
        /// </summary>
        public string name { get; set; }
    }
}
