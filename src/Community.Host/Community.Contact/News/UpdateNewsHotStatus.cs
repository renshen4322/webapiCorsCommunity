using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.News
{

 public   class UpdateNewsHotStatus:CommandRequestDto
    {
        /// <summary>
        /// 新闻id
        /// </summary>
        [Required]
        public Guid id { get; set; }
        /// <summary>
        /// 是否热门
        /// </summary>
        [Required]
        public bool is_hot { get; set; }
    }
 public class UpdateNewsHotStatusResponse : CommandResponseDto
 {
 }
}
