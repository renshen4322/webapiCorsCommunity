using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Category
{
    /// <summary>
    /// 更新分类启用状态
    /// </summary>
   public class UpdateCategoryOffLineStatus:BaseRequestDto
    {
        /// <summary>
        /// 分类id
        /// </summary>
        [Required] 
       public int id { get; set; }
       /// <summary>
       /// 是否下线
       /// </summary>
        public bool off_line { get; set; }
    }
   public class UpdateCategoryOffLineStatusResponse : BaseResponseDto
   {
   }
}
