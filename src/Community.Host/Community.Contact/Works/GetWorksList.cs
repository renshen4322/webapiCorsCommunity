using Community.Contact.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Works
{
   public class GetWorksList:QueryRequestDto
    {
        /// <summary>
        /// 用户名或用户id或作品名或作品id
        /// </summary>
        public string q { get; set; }

        /// <summary>
        /// 筛选条件 所有|热门|非热门
        /// </summary>  
        [Required]
        public HotEnum type { get; set; }
        /// <summary>
        /// 分类id (多个用英文逗号分割)
        /// </summary>
        public string category_id { get; set; }
        /// <summary>
        /// 筛选条件 所有|下线|未下线
        /// </summary>
        [Required]
        public OffLineEnum off_line { get; set; }
    }

   public class GetWorksListResponse : QueryResponseDto {
       public GetWorksListResponse()
        {
            this.data = new List<WorksInfo>();
        }
        public List<WorksInfo> data { get; set; }
   }
    public class WorksInfo{
       /// <summary>
       /// 作品id
       /// </summary>
       public Guid works_id { get; set; }
       /// <summary>
       /// 作品名
       /// </summary>
       public string works_name { get; set; }
       /// <summary>
       /// 作者
       /// </summary>
       public string owner { get; set; }
       /// <summary>
       /// 作者id
       /// </summary>
       public Guid owner_id { get; set; }
       /// <summary>
       /// 作品缩略图
       /// </summary>
       public string thumbnail { get; set; }
       /// <summary>
       /// 发布时间
       /// </summary>
       public long created_at { get; set; }
       /// <summary>
       /// 是否热门
       /// </summary>
       public bool is_hot { get; set; }
       /// <summary>
       /// 是否下线
       /// </summary>
       public bool off_line { get; set; }
   }
}
