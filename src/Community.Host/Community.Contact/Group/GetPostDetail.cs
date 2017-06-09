using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
   public class GetPostDetailRequestDto:CommandRequestDto
    {
       public int post_id { get; set; }
    }
   public class GetPostDetailResponseDto : CommandResponseDto
   {
       /// <summary>
       /// 帖子id
       /// </summary>
       public int post_id { get; set; }
       /// <summary>
       /// 帖子内容
       /// </summary>
       public string content { get; set; }

       /// <summary>
       /// 喜欢总数
       /// </summary>
       public int like_count { get; set; }
       /// <summary>
       /// 评论总数
       /// </summary>
       public int commment_count { get; set; }
       /// <summary>
       /// 收藏数
       /// </summary>
       public int collect_count { get; set; }
     
       /// <summary>
       /// 发帖时间
       /// </summary>
       public long created_at { get; set; }
   }
}
