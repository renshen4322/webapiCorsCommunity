using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Comment
{
   public class DeleteCommentRequestDto : CommandRequestDto
    {
       /// <summary>
       /// 评论id
       /// </summary>
       [Required]
       public Guid Comment_id { get; set; }
       /// <summary>
       /// 删除原因
       /// </summary>
        [Required]
       public string reason { get; set; }


    }

    public class DeleteCommentResponseDto : CommandResponseDto
    {
        /// <summary>
        /// 0:成功|-1:失败
        /// </summary>
        public int success { get; set; }
        public string msg { get; set; }
    }
}
