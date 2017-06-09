using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Product
{
    public class UpdateProductOffLineStatus : CommandRequestDto
    {
        /// <summary>
        /// 是否下线
        /// </summary>
         [Required]
        public bool off_line { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
         [Required]
        public Guid product_id { get; set; }
    }
    public class UpdateProductOffLineStatusResponse : CommandResponseDto
    {
    }
}
