using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.News
{
   public class UpdateNews:CommandRequestDto
    {
       /// <summary>
       /// id
       /// </summary>
       [Required]
       public Guid id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        public string title { get; set; }
        /// <summary>
        /// 新闻链接
        /// </summary>
        [Required]
        public string news_url { get; set; }
        /// <summary>
        /// 是否热门
        /// </summary>
        [Required]
        public bool is_hot { get; set; }
        /// <summary>
        /// 是否下线
        /// </summary>
        [Required]
        public bool off_line { get; set; }
        /// <summary>
        /// 缩略图地址
        /// </summary> 
        [Required]
        public string thumbnail_url { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [Required]
        public string introduction { get; set; }
        /// <summary>
        /// 分类id
        /// </summary>
        [Required]
        public int type_id { get; set; }
    }
   public class UpdateNewsResponse : CommandRequestDto
   {
   }
}
