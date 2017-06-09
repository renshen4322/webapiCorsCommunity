using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
    public class GetGroupPostListRequestDto:QueryRequestDto
    {
        /// <summary>
        /// 组id
        /// </summary>
        public int group_id { get; set; }
    }
    public class GetGroupPostListResponseDto : QueryResponseDto
    {

        public GetGroupPostListResponseDto()
        {
            data = new List<PostInfo>();
        }
        public List<PostInfo> data { get; set; }

    }
    public class PostInfo
    {
        /// <summary>
        /// 帖子id
        /// </summary>
        public int post_id { get; set; }
        /// <summary>
        /// 组id
        /// </summary>
        public int group_id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

     
        /// <summary>
        /// 发帖时间
        /// </summary>
        public long created_at { get; set; }
    }
   
}
