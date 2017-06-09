using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
   public class GetGroupListRequestDto:CommandRequestDto
    {
       /// <summary>
       /// 板块id
       /// </summary>
       [Required]
       public int classify_id { get; set; }
    }
   public class GetGroupListResponseDto : CommandResponseDto
   {
       public GetGroupListResponseDto()
        {
            data = new List<GroupDetailInfo>();
        }
        public List<GroupDetailInfo> data { get; set; }
   }

    public class GroupDetailInfo
    {
        /// <summary>
        /// 组id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 组名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        public string cover_url { get; set; }
        /// <summary>
        /// 是否热门
        /// </summary>
        public bool is_hot { get; set; }
        /// <summary>
        /// 板块id
        /// </summary>
        public int classify_id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long created_at { get; set; }
    }
}
