using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.News.Component
{
   public class UpdateNewsHotStatus:CommandRequestDto
    {
       /// <summary>
       /// 新闻id
       /// </summary>
       [Required]
       public Guid news_id { get; set; }
       /// <summary>
       /// 是否设为热门
       /// </summary>
       [Required]
       public bool is_hot { get; set; }
    }
   public class UpdateNewsHotStatusResponse : CommandRequestDto
   {
   }
}
