using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Category
{
    /// <summary>
    /// 更新分类热门状态
    /// </summary>
  public  class UpdateCategoryHotStatus:BaseRequestDto
    {
      /// <summary>
      /// 分类id
      /// </summary>
      [Required]
      public int id { get; set; }
      /// <summary>
      /// 是否热门
      /// </summary>
      [Required]
      public bool is_hot { get; set; }
    }
  public class UpdateCategoryHotStatusResponse : BaseResponseDto { }
}
