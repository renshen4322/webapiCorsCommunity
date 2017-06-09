using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Community.Contact.Comment;
using Community.IService;

namespace Community.Host.Controllers
{
    /// <summary>
    /// 评论相关接口
    /// </summary>
   [RoutePrefix("v1")]
    public class CommentController : ApiController
    {
         private ICommentService _commentService;

         public CommentController(ICommentService commentService)
        {
            this._commentService = commentService;
        }
        /// <summary>
        /// 获取资源评论列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("comment/{target_type}")]
        [ResponseType(typeof(GetCommentResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetCommentRequestDto dto)
        {
            GetCommentResponseDto resp = await _commentService.GetCommentListAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 获取评论举报列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("comment/report/list")]
        [ResponseType(typeof(GetReportListResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetReportListRequestDto dto)
        {
            GetReportListResponseDto resp = await _commentService.GetReportListAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 删除评论
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("comment/deleted")]
        [ResponseType(typeof(DeleteCommentResponseDto))]
        public async Task<IHttpActionResult> Post([FromBody]DeleteCommentRequestDto dto)
        {
            DeleteCommentResponseDto resp = await _commentService.DeleteCommentByIdAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 举报审核
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("comment/report/audit")]
        [ResponseType(typeof(ReportAuditResponseDto))]
        public async Task<IHttpActionResult> Delete([FromBody]ReportAuditRequestDto dto)
        {
            ReportAuditResponseDto resp = await _commentService.AuditReportAsync(dto);
            return Ok(resp);
        }
    }
}
