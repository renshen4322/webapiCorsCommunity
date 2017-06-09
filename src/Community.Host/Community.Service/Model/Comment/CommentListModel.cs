using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model.Comment
{
    public class CommentListModel
    {
        /// <summary>
        /// 作品id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 作品标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 评论作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 评论id
        /// </summary>
        public Guid CommentId { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
