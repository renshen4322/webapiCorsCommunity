using Community.Contact.Enum;
using Community.Contact.News.Component;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.News
{
    public class GetNewsList : QueryRequestDto
    {
        /// <summary>
        /// 分类id (多个用英文逗号分割)
        /// </summary>
       public string type { get; set; }
        /// <summary>
        /// 热门状态
        /// </summary>
        [Required]
       public HotEnum hot_type { get; set; }
        /// <summary>
        /// 下线状态
        /// </summary>
         [Required]
       public OffLineEnum off_line { get; set; }
    }
    public class GetNewsListResponse : QueryResponseDto
   {
        public GetNewsListResponse()
        {
            data = new List<NewsIntro>();
        }
        public List<NewsIntro> data { get; set; }
   }
    
}
