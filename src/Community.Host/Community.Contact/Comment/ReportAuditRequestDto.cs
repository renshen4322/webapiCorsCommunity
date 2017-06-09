using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using Community.Contact.Comment.Enum;

namespace Community.Contact.Comment
{
   
    public class ReportAuditRequestDto : CommandRequestDto
    {
        /// <summary>
        /// 举报编号(多个用英文逗号分割)
        /// </summary>
        [Required]
        public string report_id { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
         [Required]
        public AuditStatusEnum status { get; set; }

    }
     
    public class ReportAuditResponseDto : CommandResponseDto
    {
        /// <summary>
        /// 0:成功|-1:失败
        /// </summary>
        public int success { get; set; }
        /// <summary>
        /// 失败原因
        /// </summary>
        public string msg { get; set; }
    }
}
