using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
   public class DeletePostRequestDto:CommandRequestDto
    {
       /// <summary>
       /// 帖子id
       /// </summary>
       [Required]
       public int post_id { get; set; }
       /// <summary>
       /// 下线
       /// </summary>
        [Required]
       public bool is_offline { get; set; }
    }
   public class DeletePostResponseDto : CommandResponseDto
   {
     
   }
}
