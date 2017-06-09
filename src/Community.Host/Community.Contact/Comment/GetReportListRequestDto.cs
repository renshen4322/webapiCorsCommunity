using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Contact.Comment.Enum;

namespace Community.Contact.Comment
{
   public class GetReportListRequestDto:QueryRequestDto
    {
        ///// <summary>
        ///// 筛选类型
        ///// </summary>
        //[Required]
        //public TargetTypeEnum target_type { get; set; }
    }
   public class GetReportListResponseDto : QueryResponseDto
   {
       public GetReportListResponseDto()
        {
            data = new List<ReportDto>();
        }

       public List<ReportDto> data { get; set; }
   }

    public class ReportDto
    {
        /// <summary>
        /// 评论id
        /// </summary>
        public Guid comment_id { get; set; }
        /// <summary>
        /// 举报id
        /// </summary>
        public int report_id { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string comment_content { get; set; }
        /// <summary>
        /// 举报人
        /// </summary>
        public string author_name { get; set; }
        /// <summary>
        /// 举报人id
        /// </summary>
        public Guid author_id { get; set; }
        /// <summary>
        /// 举报时间
        /// </summary>
        public long created_at { get; set; }
        /// <summary>
        /// 举报原因
        /// </summary>
        public string report_reason { get; set; }
    }
}
