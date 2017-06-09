using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Image
{
    public class AddProject : CommandRequestDto
    {
        /// <summary>
        /// 项目名
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string name { get; set; }
    }
    public class AddProjectResponse : CommandRequestDto
    {
        public int id { get; set; }
    }
}
