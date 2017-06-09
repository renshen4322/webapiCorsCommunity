using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Community.Contact.Comment;
using Community.Contact.Group;
using Community.IService;

namespace Community.Host.Controllers
{
    /// <summary>
    /// 评论相关接口
    /// </summary>
    [RoutePrefix("v1")]
    public class GroupController : ApiController
    {
          private IGroupService _groupService;

          public GroupController(IGroupService groupService)
        {
            this._groupService = groupService;
        }

        /// <summary>
        /// 板块创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("group/classify")]
        [ResponseType(typeof(CreateClassifyResponseDto))]
        public async Task<IHttpActionResult> Post([FromBody]CreateClassifyRequestDto dto)
        {
            var resp = await _groupService.CreateClassifySync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 更新板块信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("group/classify")]
        [ResponseType(typeof(UpdateClassifyResponseDto))]
        public async Task<IHttpActionResult> Put([FromBody]UpdateClassifyRequestDto dto)
        {
            var resp = await _groupService.UpdateClassifySync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 组创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("group")]
        [ResponseType(typeof(CreateGroupResponseDto))]
        public async Task<IHttpActionResult> Get([FromBody]CreateGroupRequestDto dto)
        {
            var resp = await _groupService.CreateGroupSync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 更新组信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("group")]
        [ResponseType(typeof(UpdateGroupResponseDto))]
        public async Task<IHttpActionResult> Put([FromBody]UpdateGroupRequestDto dto)
        {
            var resp = await _groupService.UpdateGroupSync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 获取所有板块
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("group/classify/list")]
        [ResponseType(typeof(GetClassifyListResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetClassifyListRequestDto dto)
        {
            var resp = await _groupService.GetAllClassifySync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 获取板块组信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("group/classify/{classify_id:int}/group/list")]
        [ResponseType(typeof(GetGroupListResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetGroupListRequestDto dto)
        {
            var resp = await _groupService.GetGroupListByClassifyIdSync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 获取帖子详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("group/post/{post_id:int}/detail")]
        [ResponseType(typeof(GetPostDetailResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetPostDetailRequestDto dto)
        {
            var resp = await _groupService.GetPostDetailByIdSync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 获取小组帖子列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("group/{group_id:int}/post/list")]
        [ResponseType(typeof(GetGroupPostListResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetGroupPostListRequestDto dto)
        {
            var resp = await _groupService.GetPostListByGroupIdSync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 根据标题模糊筛选帖子
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("group/post/search")]
        [ResponseType(typeof(GetPostListByQueryResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetPostListByQueryRequestDto dto)
        {
            var resp = await _groupService.GetPostListByTitleSycn(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 帖子上/下线
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("group/post")]
        [ResponseType(typeof(DeletePostResponseDto))]
        public async Task<IHttpActionResult> Put([FromBody]DeletePostRequestDto dto)
        {
            var resp = await _groupService.UpdatePostOffLineStatusSync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 小组上/下线
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("group/offline")]
        [ResponseType(typeof(RemoveGroupResponseDto))]
        public async Task<IHttpActionResult> Put([FromBody]RemoveGroupRequestDto dto)
        {
            var resp = await _groupService.UpdatePostOffLineStateSync(dto);
            return Ok(resp);
        }
        
       
      
       
       
        /// <summary>
        /// 删除板块
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("group/classify/{classify_id:int}")]
        [ResponseType(typeof(RemoveClassifyResponseDto))]
        public async Task<IHttpActionResult> Delete([FromUri]RemoveClassifyRequestDto dto)
        {
            var resp = await _groupService.DeleteClassifySync(dto);
            return Ok(resp);
        }
       
        /// <summary>
        /// 更新板块排序
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("group/classify/order")]
        [ResponseType(typeof(UpdateClassifyOrderResponseDto))]
        public async Task<IHttpActionResult> Put([FromBody]UpdateClassifyOrderRequestDto dto)
        {
            var resp = await _groupService.UpdateClassifyOrderSync(dto);
            return Ok(resp);
        }
       
    }
}
