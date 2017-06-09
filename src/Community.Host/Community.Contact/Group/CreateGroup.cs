using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
   public class CreateGroupRequestDto:CommandRequestDto
    {
       /// <summary>
       /// 组名
       /// </summary>
       [Required]
       public string name { get; set; }

       /// <summary>
       /// 所属板块id
       /// </summary>
        [Required]
       public int classify_id { get; set; }

       /// <summary>
       /// 组描述
       /// </summary>
       [Required]
       public string description { get; set; }

       /// <summary>
       /// 组封面
       /// </summary>
         [Required]
       public string cover_url { get; set; }

       /// <summary>
       /// 是否是热门组
       /// </summary>
       [Required]
       public bool is_hot { get; set; }
    }
   public class CreateGroupResponseDto : CommandResponseDto
   {
       /// <summary>
       /// 组id
       /// </summary>
       public int id { get; set; }
   }
}
