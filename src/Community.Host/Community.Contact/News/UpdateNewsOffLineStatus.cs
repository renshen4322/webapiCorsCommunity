using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.News
{
    /// <summary>
    /// 更新新闻下线状态
    /// </summary>
   public class UpdateNewsOffLineStatus:CommandRequestDto
    {
        /// <summary>
        /// 新闻id
        /// </summary>
        [Required]
        public Guid id { get; set; }
        /// <summary>
        /// 是否下线
        /// </summary>
        public bool off_line { get; set; }
    }
   public class UpdateNewsOffLineStatusResponse : CommandResponseDto
   {
   }
}
