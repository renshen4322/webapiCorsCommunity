using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Works
{
    public class UpdateWorksOffLineStatus : CommandRequestDto
    {
        /// <summary>
        /// 是否下线
        /// </summary>
         [Required]
        public bool off_line { get; set; }
        /// <summary>
        /// 作品id
        /// </summary>
         [Required]
        public Guid works_id { get; set; }
    }
    public class UpdateWorksOffLineStatusResponse : CommandResponseDto
    {

    }
}
