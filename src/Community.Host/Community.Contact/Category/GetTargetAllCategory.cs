using Community.Contact.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Category
{
    public class GetTargetAllCategory : BaseRequestDto
    {
        [Required]
        public CategoryTypeEnum type { get; set; }
    }
    public class GetTargetAllCategoryResponse : BaseResponseDto
    {
        public GetTargetAllCategoryResponse()
        {
            this.category_list = new List<CategoryList>();
        }
        public List<CategoryList> category_list { get; set; }
    }
    public class CategoryList
    {
        public CategoryList()
        {
            this.list = new List<Category>();
        }
        public string type { get; set; }
        public int type_id { get; set; }
        public bool is_multiple { get; set; }
        public List<Category> list { get; set; }
    }
    public class Category
    {
        /// <summary>
        /// 分类id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 中文名
        /// </summary>
        public string cn_name { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string en_name { get; set; }
        /// <summary>
        /// 使用状态，false:启用｜true:弃用
        /// </summary>
        public bool off_line { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        public bool is_hot { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public int display_index { get; set; }
    }

}
