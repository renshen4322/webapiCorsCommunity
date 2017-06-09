using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model
{
  public  class SearchWorksModel
    {
        /// <summary>
        /// 作品id
        /// </summary>
        public Guid WorksId { get; set; }
        /// <summary>
        /// 作品名
        /// </summary>
        public string WorksName { get; set; }
        /// <summary>
        /// 作者名称
        /// </summary>              
        public string Owner { get; set; }

        /// <summary>
        /// 作者id
        /// </summary>
        public Guid OwnerId { get; set; }
     

        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction { get; set; }
        /// <summary>
        /// 作品缩略图
        /// </summary>
        public string Thumbnail { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// 是否热门
        /// </summary>
        public bool IsHot { get; set; }
        /// <summary>
        /// 是否下线
        /// </summary>
        public bool OffLine { get; set; }

    }
}
