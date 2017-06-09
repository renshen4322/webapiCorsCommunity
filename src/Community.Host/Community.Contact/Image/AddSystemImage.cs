using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Image
{
    /// <summary>
    /// 添加新图片
    /// </summary>
   public class AddSystemImage:CommandRequestDto
    {
        /// <summary>
        /// 项目id
        /// </summary>
        [Required]
        public int project_id { get; set; }
        /// <summary>
        /// 图片名
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string image_name { get; set; }
        /// <summary>
        /// 使用描述
        /// </summary>
        [MaxLength(50)]
        public string description { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string image_uri { get; set; }
      
    }
   public class AddSystemImageResponse : CommandRequestDto
   {
       /// <summary>
       /// 图片id
       /// </summary>
       public int id { get; set; }
   }
}
