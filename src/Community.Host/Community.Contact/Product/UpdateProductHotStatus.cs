using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Product
{
   public class UpdateProductHotStatus:CommandRequestDto
    {
       /// <summary>
       /// 是否热门
       /// </summary>
        [Required]
       public bool is_hot { get; set; }
       /// <summary>
       /// 产品id
       /// </summary>
        [Required]
       public Guid product_id { get; set; }
    }
   public class UpdateProductHotStatusResponse : CommandResponseDto { 
   
   }
}
