using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Works
{
   public class UpdateWorksHotStatus:CommandRequestDto
    {
        /// <summary>
        /// 是否热门
        /// </summary>
         [Required]
        public bool is_hot { get; set; }
        /// <summary>
        /// 作品id
        /// </summary>
         [Required]
        public Guid works_id { get; set; }
    }
   public class UpdateWorksHotStatusResponse : CommandResponseDto { 
   }
}
