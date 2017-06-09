using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Works.Component
{
   public class ItemInfo
    {
        /// <summary>
        /// 物品id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 作品id
        /// </summary>
        public Guid works_id { get; set; }
        /// <summary>
        /// pc端中的拥有者
        /// </summary>
        public int owner_id { get; set; }
        /// <summary>
        /// 产品导入时ID
        /// </summary>
        public int product_id { get; set; }
        /// <summary>
        /// 产品图片
        /// </summary>
        public string product_url { get; set; }
        /// <summary>
        /// 产品名
        /// </summary>
        public string product_name { get; set; }
    }
}
