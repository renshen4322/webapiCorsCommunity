using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Contact.Group;

namespace Community.IService
{
    /// <summary>
    /// 小组板块接口定义
    /// </summary>
    public interface IGroupService
    {
        /// <summary>
        /// 创建板块
        /// </summary>
        /// <param name="dto">CreateClassifyRequestDto</param>
        /// <returns>CreateClassifyResponseDto</returns>
        Task<CreateClassifyResponseDto> CreateClassifySync(CreateClassifyRequestDto dto);

        /// <summary>
        /// 修改板块信息
        /// </summary>
        /// <param name="dto">UpdateClassifyRequestDto</param>
        /// <returns>UpdateClassifyResponseDto</returns>
        Task<UpdateClassifyResponseDto> UpdateClassifySync(UpdateClassifyRequestDto dto);
        /// <summary>
        /// 创建小组
        /// </summary>
        /// <param name="dto">CreateGroupRequestDto</param>
        /// <returns>CreateGroupResponseDto</returns>
        Task<CreateGroupResponseDto> CreateGroupSync(CreateGroupRequestDto dto);
        /// <summary>
        /// 更新小组信息
        /// </summary>
        /// <param name="dto">UpdateGroupRequestDto</param>
        /// <returns>UpdateGroupResponseDto</returns>
        Task<UpdateGroupResponseDto> UpdateGroupSync(UpdateGroupRequestDto dto);

        /// <summary>
        /// 获取所有板块信息
        /// </summary>
        /// <param name="dto">GetClassifyListRequestDto</param>
        /// <returns>GetClassifyListResponseDto</returns>
        Task<GetClassifyListResponseDto> GetAllClassifySync(GetClassifyListRequestDto dto);
        /// <summary>
        /// 获取板块组列表
        /// </summary>
        /// <param name="dto">GetGroupListRequestDto</param>
        /// <returns>GetGroupListResponseDto</returns>
        Task<GetGroupListResponseDto> GetGroupListByClassifyIdSync(GetGroupListRequestDto dto);

        /// <summary>
        /// 获取小组帖子列表
        /// </summary>
        /// <param name="dto">GetGroupPostListRequestDto</param>
        /// <returns>GetGroupPostListResponseDto</returns>
        Task<GetGroupPostListResponseDto> GetPostListByGroupIdSync(GetGroupPostListRequestDto dto);

        /// <summary>
        /// 获取帖子详情
        /// </summary>
        /// <param name="dto">GetPostDetailRequestDto</param>
        /// <returns>GetPostDetailResponseDto</returns>
        Task<GetPostDetailResponseDto> GetPostDetailByIdSync(GetPostDetailRequestDto dto);

        /// <summary>
        /// 通过标题搜索帖子
        /// </summary>
        /// <param name="dto">GetPostListByQueryRequestDto</param>
        /// <returns>GetPostListByQueryResponseDto</returns>
        Task<GetPostListByQueryResponseDto> GetPostListByTitleSycn(GetPostListByQueryRequestDto dto);

        /// <summary>
        /// 更新帖子下线状态
        /// </summary>
        /// <param name="dto">DeletePostRequestDto</param>
        /// <returns>DeletePostResponseDto</returns>
        Task<DeletePostResponseDto> UpdatePostOffLineStatusSync(DeletePostRequestDto dto);

        /// <summary>
        /// 更新板块排序信息
        /// </summary>
        /// <param name="dto">UpdateClassifyOrderRequestDto</param>
        /// <returns>UpdateClassifyOrderResponseDto</returns>
        Task<UpdateClassifyOrderResponseDto> UpdateClassifyOrderSync(UpdateClassifyOrderRequestDto dto);

        /// <summary>
        /// 删除板块
        /// </summary>
        /// <param name="dto">RemoveClassifyRequestDto</param>
        /// <returns>RemoveClassifyResponseDto</returns>
        Task<RemoveClassifyResponseDto> DeleteClassifySync(RemoveClassifyRequestDto dto);
        /// <summary>
        /// 更新小组上下线状态
        /// </summary>
        /// <param name="dto">RemoveGroupRequestDto</param>
        /// <returns>RemoveGroupResponseDto</returns>
        Task<RemoveGroupResponseDto> UpdatePostOffLineStateSync(RemoveGroupRequestDto dto);
    }
}
