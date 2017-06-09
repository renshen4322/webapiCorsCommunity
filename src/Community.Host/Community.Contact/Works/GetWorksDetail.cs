using Community.Contact.Works.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Works
{
   public class GetWorksDetail:BaseRequestDto
    {
       public Guid id { get; set; }
    }
   public class GetWorksDetailResponse : BaseResponseDto
   {
       /// <summary>
       /// 作品id
       /// </summary>
       public Guid id { get; set; }
       /// <summary>
       /// 所有者id
       /// </summary>
       public Guid owner_id { get; set; }
       /// <summary>
       /// 作品名
       /// </summary>      
       public string name { get; set; }

       /// <summary>
       /// 设计时间
       /// </summary>      
       public long design_date { get; set; }
       /// <summary>
       /// 造价
       /// </summary>
       public float? cost { get; set; }

       /// <summary>
       /// 实际面积
       /// </summary>
       public float actual_area { get; set; }
       /// <summary>
       /// 缩略图
       /// </summary>
       public string thumbnail { get; set; }
       /// <summary>
       /// 作品简介
       /// </summary>     
       public string introduction { get; set; }

       /// <summary>
       /// 全景图地址集合，使用逗号分隔多个全景图
       /// </summary>
       public string pano_url { get; set; }

       /// <summary>
       /// 图片集合，使用逗号分隔多个全景图
       /// </summary>
       public string images { get; set; }

       /// <summary>
       /// 物品清单
       /// </summary>
       public List<ItemInfo> products { get; set; }

       /// <summary>
       /// 详细描述
       /// </summary>    
       public string description { get; set; }
       /// <summary>
       /// 参与者
       /// </summary>
       public string helper { get; set; }

       /// <summary>
       /// 热门状态
       /// </summary>
       public bool is_hot { get; set; }
       /// <summary>
       /// 下线状态
       /// </summary>
       public bool off_line { get; set; }

       /// <summary>
       /// 发布时间
       /// </summary>
       public long created_at { get; set; }
   }
}
