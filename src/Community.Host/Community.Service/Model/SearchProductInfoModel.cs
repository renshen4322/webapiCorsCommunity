using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model
{
  public  class SearchProductInfoModel
    {
        /// <summary>
        /// 产品id
        /// </summary>
      public Guid ProductId { get; set; }
     
        /// <summary>
        /// 产品名
        /// </summary>
      public string ProductName { get; set; }
        /// <summary>
        /// 所有者昵称
        /// </summary>              
      public string Owner { get; set; }
        /// <summary>
        /// 所有者id
        /// </summary>
      public Guid OwnerId { get; set; }     

        /// <summary>
        /// 简介
        /// </summary>
      public string Introduction { get; set; }
        /// <summary>
        /// 产品缩略图
        /// </summary>
      public string Thumbnail { get; set; }
      public bool IsHot { get; set; }
      public bool OffLine { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
      public DateTime CreatedDate { get; set; }
    }
}
