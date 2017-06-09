using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Category
{
    /// <summary>
    /// 更新分类优先级
    /// </summary>
   public class UpdateCategoryDisplayIndex:BaseRequestDto
    {
       /// <summary>
       /// 分类id
       /// </summary>
        [Required]
       public int id { get; set; }
       /// <summary>
       /// 优先级序号
       /// </summary>
        [Required]
        public int display_index { get; set; }
    }
   public class UpdateCategoryDisplayIndexResponse : BaseRequestDto { }
}
