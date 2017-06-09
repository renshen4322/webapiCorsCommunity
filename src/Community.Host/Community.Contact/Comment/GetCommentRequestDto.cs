using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Contact.Comment.Enum;
using System.ComponentModel.DataAnnotations;

namespace Community.Contact.Comment
{
    /// <summary>
    /// 获取评论信息
    /// </summary>
    public class GetCommentRequestDto : QueryRequestDto
    {
       /// <summary>
       /// 筛选类型
       /// </summary>
       public TargetTypeEnum target_type { get; set; }
       /// <summary>
       /// 资源id或标题
       /// </summary>
       public string q { get; set; }
    }

    public class GetCommentResponseDto : QueryResponseDto
    {
        public GetCommentResponseDto()
        {
            data = new List<CommentDto>();
        }

        public List<CommentDto> data { get; set; }
    }

    public class CommentDto
    {
        /// <summary>
        /// 作品id
        /// </summary>
        public Guid id { get; set; }
        /// <summary>
        /// 作品标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 评论作者
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 评论id
        /// </summary>
        public Guid comment_id { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public long created_at { get; set; }
    }
}
