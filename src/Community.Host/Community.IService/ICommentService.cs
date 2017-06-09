using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Contact.Comment;

namespace Community.IService
{
    /// <summary>
    /// 评论模块接口
    /// </summary>
   public interface ICommentService
    {
       
         Task<GetCommentResponseDto> GetCommentListAsync(Contact.Comment.GetCommentRequestDto dto);

         Task<DeleteCommentResponseDto> DeleteCommentByIdAsync(Contact.Comment.DeleteCommentRequestDto dto);

        Task<GetReportListResponseDto> GetReportListAsync(Contact.Comment.GetReportListRequestDto dto);

        Task<ReportAuditResponseDto> AuditReportAsync(Contact.Comment.ReportAuditRequestDto dto);
      
       

    }
}
