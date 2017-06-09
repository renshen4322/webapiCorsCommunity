using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.News.Component
{
   public class UpdateNewsOffLineStatus:CommandRequestDto
    {
       /// <summary>
       /// 新闻id
       /// </summary>
       [Required]
       public Guid news_id { get; set; }
       /// <summary>
       /// 新闻下线状态
       /// </summary>
       [Required]
       public bool off_line { get; set; }
    }
   public class UpdateNewsOffLineStatusResponse : CommandResponseDto
   {
   }
}
