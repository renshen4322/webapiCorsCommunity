using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communiry.Entity.Group;
using Community.Common.Exception;
using Community.Contact.Group;
using Community.Core.Data;
using Community.IService;
using AutoMapper;

namespace Community.Service
{
  public class GroupService:IGroupService
  {
      #region Fields
      private readonly IDapperRepository _dapperRepository;
      private readonly IRepository<GroupClassifyEntity> _groupClassifyRepository;
      private readonly IRepository<GroupInfoEntity> _groupInfoRepository;
      private readonly IRepository<GroupPostEntity> _groupPostRepository;
      private readonly IRepository<GroupUserEntity> _groupUserRepository;
      private readonly IRepository<GroupPostContentEntity> _groupPostContentRepository;
      private readonly IRepository<GroupCommentEntity> _groupCommentRepository;

      #endregion

      #region Ctor
      public GroupService(IDapperRepository dapperRepository,
          IRepository<GroupClassifyEntity> groupClassifyRepository,
          IRepository<GroupInfoEntity> groupInfoRepository,
          IRepository<GroupPostEntity> groupPostRepository,
            IRepository<GroupPostContentEntity> groupPostContentRepository,
          IRepository<GroupUserEntity> groupUserRepository,
          IRepository<GroupCommentEntity> groupCommentRepository
          )
      {
          _dapperRepository = dapperRepository;
          _groupClassifyRepository = groupClassifyRepository;
          _groupInfoRepository = groupInfoRepository;
          _groupPostRepository = groupPostRepository;
          _groupUserRepository = groupUserRepository;
          _groupPostContentRepository = groupPostContentRepository;
          _groupCommentRepository = groupCommentRepository;
      }

      #endregion

      #region Method

      public Task<CreateClassifyResponseDto> CreateClassifySync(CreateClassifyRequestDto dto)
      {
          return Task.Run(() =>
          {
              CreateClassifyResponseDto resp=new CreateClassifyResponseDto();
              var classifyEntity = new GroupClassifyEntity()
              {
                  Description = dto.description,
                  GMTCreate = DateTime.Now,
                  IsOffLine = false,
                  Name = dto.name,
                  Order = dto.order
              };
              _groupClassifyRepository.Insert(classifyEntity);
              resp.id = classifyEntity.Id;
              return resp;
          });

      }

        public Task<UpdateClassifyResponseDto> UpdateClassifySync(UpdateClassifyRequestDto dto)
        {
            return Task.Run(() =>
            {
                UpdateClassifyResponseDto resp = new UpdateClassifyResponseDto();
                var classifyEntity = _groupClassifyRepository.Table.SingleOrDefault(t => t.Id.Equals(dto.id));
                if (classifyEntity!=null)
                {
                    classifyEntity.Name = dto.name;
                    classifyEntity.Description = dto.description;
                    classifyEntity.Order = dto.order;
                    classifyEntity.GMTModified=DateTime.Now;
                    
                    _groupClassifyRepository.Update(classifyEntity);
                }
                else
                {
                    throw new NotFoundException("不存在该板块");
                }
                return resp;
            });
        }

        public Task<CreateGroupResponseDto> CreateGroupSync(CreateGroupRequestDto dto)
        {
            return Task.Run(() =>
            {

                CreateGroupResponseDto resp = new CreateGroupResponseDto();
              var classifyEntity=  _groupClassifyRepository.TableNoTracking.SingleOrDefault(t => t.Id.Equals(dto.classify_id));
              if (classifyEntity!=null)
                {
                    var groupEntity = new GroupInfoEntity()
                    {
                        ClassifyId = dto.classify_id,
                        CoverUrl = dto.cover_url,
                        CreatedUser = Guid.Empty,
                        Description = dto.description,
                        GMTCreate = DateTime.Now,
                        IsOffLine = false,
                        IsHot = dto.is_hot,
                        Name = dto.name
                    };
                    _groupInfoRepository.Insert(groupEntity);
                    resp.id = groupEntity.Id;
                }
              else
              {
                  throw new NotFoundException("所属板块不存在");
              }
              
                return resp;

            });
        }

        public Task<UpdateGroupResponseDto> UpdateGroupSync(UpdateGroupRequestDto dto)
        {
            return Task.Run(() =>
            {

                UpdateGroupResponseDto resp = new UpdateGroupResponseDto();
                var groupEntity = _groupInfoRepository.Table.SingleOrDefault(t => t.Id.Equals(dto.group_id));
                if (groupEntity != null)
                {
                    groupEntity.CoverUrl = dto.cover_url;
                    groupEntity.Description = dto.description;
                    groupEntity.GMTModified=DateTime.Now;
                    groupEntity.IsHot = dto.is_hot;
                    groupEntity.Name = dto.name;
                    _groupInfoRepository.Update(groupEntity);
                    
                }
                else
                {
                    throw new NotFoundException("不存在待更新的小组");
                }

                return resp;

            });
        }

        public Task<GetClassifyListResponseDto> GetAllClassifySync(GetClassifyListRequestDto dto)
        {
            return Task.Run(() =>
            {
                GetClassifyListResponseDto resp=new GetClassifyListResponseDto();
                var classifyEntities = _groupClassifyRepository.TableNoTracking.Where(t => !t.IsOffLine);
                if (classifyEntities.Any())
                {
                    resp.data = Mapper.Map<List<Classify>>(classifyEntities);
                }
                return resp;
            });
        }

        public Task<GetGroupListResponseDto> GetGroupListByClassifyIdSync(GetGroupListRequestDto dto)
        {
            return Task.Run(() =>
            {
                GetGroupListResponseDto resp=new GetGroupListResponseDto();
                var groupEntities =_groupInfoRepository.TableNoTracking.Where(t => t.ClassifyId.Equals(dto.classify_id) && !t.IsOffLine);
                if (groupEntities.Any())
                {
                    resp.data = Mapper.Map<List<GroupDetailInfo>>(groupEntities);
                }
                return resp;
            });
        }
        public Task<GetPostDetailResponseDto> GetPostDetailByIdSync(GetPostDetailRequestDto dto)
        {
            return Task.Run(() =>
            {
                
                var postInfoEntity = _groupPostRepository.TableNoTracking.SingleOrDefault(t => t.Id.Equals(dto.post_id));
                if (postInfoEntity!=null)
                {
                    GetPostDetailResponseDto resp = Mapper.Map<GetPostDetailResponseDto>(postInfoEntity);
                    var postContentEntity=_groupPostContentRepository.TableNoTracking.SingleOrDefault(t => t.PostId.Equals(dto.post_id));
                    resp.content = postContentEntity.Content;
                    return resp;
                }
                throw new NotFoundException("不存在该帖子");


            });
        }

        public Task<GetGroupPostListResponseDto> GetPostListByGroupIdSync(GetGroupPostListRequestDto dto)
        {
            return Task.Run(() =>
            {
                GetGroupPostListResponseDto resp=new GetGroupPostListResponseDto();
                resp.total =
                    _groupPostRepository.TableNoTracking.Count(t => t.GroupId.Equals(dto.group_id) && !t.IsOffLine);
                if (resp.total>dto.start)
                {
                    var postInfoEntities = _groupPostRepository.TableNoTracking.Where(t => t.GroupId.Equals(dto.group_id) && !t.IsOffLine).OrderByDescending(t => t.GMTCreate).Skip(dto.start).Take(dto.length);
                    if (postInfoEntities.Any())
                    {
                        resp.data = Mapper.Map<List<PostInfo>>(postInfoEntities);
                    } 
                }
                
                return resp;
            });
        }

        
        public Task<GetPostListByQueryResponseDto> GetPostListByTitleSycn(GetPostListByQueryRequestDto dto)
        {
            return Task.Run(() =>
            {
                GetPostListByQueryResponseDto resp = new GetPostListByQueryResponseDto();
                resp.total =
                    _groupPostRepository.TableNoTracking.Count(t =>t.Title.Contains(dto.q) && !t.IsOffLine);
                if (resp.total > dto.start)
                {
                    var postInfoEntities = _groupPostRepository.TableNoTracking.Where(t => t.Title.Contains(dto.q)).OrderByDescending(t => t.GMTCreate).Skip(dto.start).Take(dto.length);
                    if (postInfoEntities.Any())
                    {
                        resp.data = Mapper.Map<List<PostInfo>>(postInfoEntities);
                    }
                }

                return resp;
            });
        }

        public Task<DeletePostResponseDto> UpdatePostOffLineStatusSync(DeletePostRequestDto dto)
        {
            return Task.Run(() =>
            {
                var postInfoEntity = _groupPostRepository.Table.SingleOrDefault(t => t.Id.Equals(dto.post_id));
                if (postInfoEntity!=null)
                {
                    postInfoEntity.IsOffLine = dto.is_offline;
                    postInfoEntity.GMTModified=DateTime.Now;
                    _groupPostRepository.Update(postInfoEntity);
                }
                else
                {
                    throw new NotFoundException("找不到需要更新的帖子");
                }

                return new DeletePostResponseDto();

            });
        }

        public Task<UpdateClassifyOrderResponseDto> UpdateClassifyOrderSync(UpdateClassifyOrderRequestDto dto)
        {
            return Task.Run(() =>
            {
                var classifyEntity = _groupClassifyRepository.Table.SingleOrDefault(t => t.Id.Equals(dto.classify_id));
                if (classifyEntity!=null)
                {
                    classifyEntity.Order = dto.order;
                    classifyEntity.GMTModified=DateTime.Now;
                    _groupClassifyRepository.Update(classifyEntity);
                }
                else
                {
                    throw new NotFoundException("找不到需要更新的板块");
                }

                return new UpdateClassifyOrderResponseDto();

            });
        }

        public Task<RemoveClassifyResponseDto> DeleteClassifySync(RemoveClassifyRequestDto dto)
        {
            return Task.Run(() =>
            {
                var classifyEntity = _groupClassifyRepository.Table.SingleOrDefault(t => t.Id.Equals(dto.classify_id));
                if (classifyEntity != null)
                {
                    classifyEntity.IsOffLine =true;
                    classifyEntity.GMTModified = DateTime.Now;
                    _groupClassifyRepository.Update(classifyEntity);
                }
                else
                {
                    throw new NotFoundException("找不到需要下线的板块");
                }

                return new RemoveClassifyResponseDto();

            });
        }
        public Task<RemoveGroupResponseDto> UpdatePostOffLineStateSync(RemoveGroupRequestDto dto)
        {
            return Task.Run(() =>
            {
                RemoveGroupResponseDto resp = new RemoveGroupResponseDto();
                var groupEntity=_groupInfoRepository.Table.SingleOrDefault(t => t.Id.Equals(dto.group_id));
                if (groupEntity!=null)
                {
                    if (!groupEntity.IsOffLine.Equals(dto.offline))
                    {
                        groupEntity.IsOffLine = dto.offline;
                        _groupInfoRepository.Update(groupEntity);
                    }
                }
                else
                {
                    throw new NotFoundException("找不到需要更新的小组");
                }

                return resp;
            });
        }
      #endregion


      
  }
}
