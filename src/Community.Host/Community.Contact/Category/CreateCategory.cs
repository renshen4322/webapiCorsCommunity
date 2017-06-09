using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Category
{
    /// <summary>
    /// 添加分类
    /// </summary>
    public class CreateCategory:CommandRequestDto
    {
        /// <summary>
        /// 分类所属资源id
        /// </summary>
        public int type_id { get; set; }
        /// <summary>
        /// 分类所属父级id
        /// </summary>
        public int parent_id { get; set; }
        /// <summary>
        /// 中文名
        /// </summary>
        public string cn_name { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string en_name { get; set; }
       /// <summary>
       /// 是否启用
       /// </summary>
        public bool off_line { get; set; }
        /// <summary>
        /// 是否热门
        /// </summary>
        public bool is_hot { get; set; }
        /// <summary>
        /// 显示序列
        /// </summary>
        public int displayindex { get; set; }
        
    }
    public class CreateCategoryResponse : CommandResponseDto
    {
        public int id { get; set; }
    }
}
