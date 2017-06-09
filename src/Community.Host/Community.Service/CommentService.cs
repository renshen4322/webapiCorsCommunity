using Community.IService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Communiry.Entity;
using Communiry.Entity.Comment;
using Community.Common.Exception;
using Community.Contact.Comment;
using Community.Contact.Comment.Enum;
using Community.Core.Data;
using Community.Service.Const;
using Community.Service.Model.Comment;
using AuditStatusEnum = Community.Contact.Comment.Enum.AuditStatusEnum;

namespace Community.Service
{
    public class CommentService : ICommentService
    {
        #region Fields
        private readonly IRepository<CommentEntity> _commentRepository;
        private readonly IRepository<CommentReportEntity> _commentReportRepository;
        private readonly IRepository<CommentLikeEntity> _likeRepository;
        private readonly IRepository<CommentLikeCountEntity> _commentLikeCountRepository;
        private readonly IRepository<ResourceLikeCountEntity> _resourceLikeCountRepository;
        private readonly IDapperRepository _dapperRepository;
        private readonly IRepository<CommentDeleteHistoryEntity> _commentDeleteHistoryRepository;


        #endregion
        #region Ctor
        public CommentService(
            IRepository<CommentEntity> commentRepository,
            IDapperRepository dapperRepository,
            IRepository<CommentReportEntity> commentReportRepository,
            IRepository<CommentLikeCountEntity> commentLikeCountRepository,
            IRepository<CommentLikeEntity> likeRepository,
            IRepository<ResourceLikeCountEntity> resourceLikeCountRepository,
            IRepository<CommentDeleteHistoryEntity> commentDeleteHistoryRepository)
        {

            _commentRepository = commentRepository;
            _dapperRepository = dapperRepository;
            _commentReportRepository = commentReportRepository;
            _commentLikeCountRepository = commentLikeCountRepository;
            _likeRepository = likeRepository;
            _commentDeleteHistoryRepository = commentDeleteHistoryRepository;
            _resourceLikeCountRepository = resourceLikeCountRepository;
        }
        #endregion

        #region Method
        public Task<GetCommentResponseDto> GetCommentListAsync(GetCommentRequestDto dto)
        {
            return Task.Run(() =>
            {
                List<CommentListModel> list = new List<CommentListModel>();
                int total = 0;
                Guid qId = Guid.Empty;
                string qTitle = "";
                if (!string.IsNullOrEmpty(dto.q))
                {
                    if (!Guid.TryParse(dto.q, out qId))
                    {
                        qTitle = dto.q;
                    }

                }

                string worksSql;
                string productSql;
                string worksCountSql;
                string productCountSql;
                switch (dto.target_type)
                {
                    #region all
                    //case TargetTypeEnum.All:

                    //    worksSql = string.Format(CommentServiceConst.SELECT_WORKS_COMMENT_LIST,
                    //        qId.Equals(Guid.Empty) ? "" : CommentServiceConst.SELECT_WORKS_COMMENT_WHERE_0,
                    //        string.IsNullOrEmpty(qTitle) ? "" : CommentServiceConst.SELECT_WORKS_COMMENT_WHERE_1);
                    //    list = _dapperRepository.Query<CommentListModel>(worksSql, new
                    //    {
                    //        start = dto.start,
                    //        length = (dto.length / 2),
                    //        worksId = qId.Equals(Guid.Empty) ? "" : dto.q,
                    //        title = string.IsNullOrEmpty(qTitle) ? "" : dto.q

                    //    }).ToList();

                    //    productSql = string.Format(CommentServiceConst.SELECT_PRODUCT_COMMENT_LIST,
                    //        qId.Equals(Guid.Empty) ? "" : CommentServiceConst.SELECT_PRODUCT_COMMENT_WHERE_0,
                    //        string.IsNullOrEmpty(qTitle) ? "" : CommentServiceConst.SELECT_PRODUCT_COMMENT_WHERE_1);
                    //    list.AddRange(_dapperRepository.Query<CommentListModel>(productSql,
                    //          new
                    //          {
                    //              start = dto.start,
                    //              length = dto.length - list.Count,
                    //              worksId = qId.Equals(Guid.Empty) ? "" : dto.q,
                    //              title = string.IsNullOrEmpty(qTitle) ? "" : dto.q
                    //          }).ToList());

                    //    if (string.IsNullOrEmpty(dto.q))
                    //    {
                    //        total = _dapperRepository.ExecuteScalar<int>(CommentServiceConst.QUERY_ALL_COMMENT_COUNT);
                    //    }
                    //    else
                    //    {
                    //        worksCountSql = string.Format(CommentServiceConst.QUERY_WORKS_COMMENT_COUNT,
                    //            qId.Equals(Guid.Empty) ? "" : CommentServiceConst.SELECT_WORKS_COMMENT_WHERE_0,
                    //            string.IsNullOrEmpty(qTitle) ? "" : CommentServiceConst.SELECT_WORKS_COMMENT_WHERE_1);
                    //        productCountSql = string.Format(CommentServiceConst.QUERY_PRODUCT_COMMENT_COUNT,
                    //            qId.Equals(Guid.Empty) ? "" : CommentServiceConst.SELECT_PRODUCT_COMMENT_WHERE_0,
                    //            string.IsNullOrEmpty(qTitle) ? "" : CommentServiceConst.SELECT_PRODUCT_COMMENT_WHERE_1);
                    //        total = _dapperRepository.ExecuteScalar<int>(worksCountSql) + _dapperRepository.ExecuteScalar<int>(productCountSql);
                    //    }
                    //    break;
                    #endregion
                    case TargetTypeEnum.Works:
                        worksSql = string.Format(CommentServiceConst.SELECT_WORKS_COMMENT_LIST,
                            qId.Equals(Guid.Empty) ? "" : CommentServiceConst.SELECT_WORKS_COMMENT_WHERE_0,
                            string.IsNullOrEmpty(qTitle) ? "" : CommentServiceConst.SELECT_WORKS_COMMENT_WHERE_1);
                        list = _dapperRepository.Query<CommentListModel>(worksSql, new
                        {
                            start = dto.start,
                            length = dto.length,
                            worksId = qId.Equals(Guid.Empty) ? "" : dto.q,
                            title = string.IsNullOrEmpty(qTitle) ? "" : "%" + dto.q + "%"

                        }).ToList();
                        worksCountSql = string.Format(CommentServiceConst.QUERY_WORKS_COMMENT_COUNT,
                            qId.Equals(Guid.Empty) ? "" : CommentServiceConst.SELECT_WORKS_COMMENT_WHERE_0,
                            string.IsNullOrEmpty(qTitle) ? "" : CommentServiceConst.SELECT_WORKS_COMMENT_WHERE_1);
                        total = _dapperRepository.ExecuteScalar<int>(worksCountSql, new
                        {
                            worksId = qId.Equals(Guid.Empty) ? "" : dto.q,
                            title = string.IsNullOrEmpty(qTitle) ? "" : "%" + dto.q + "%"
                        });

                        break;

                    case TargetTypeEnum.Product:
                        productSql = string.Format(CommentServiceConst.SELECT_PRODUCT_COMMENT_LIST,
                            qId.Equals(Guid.Empty) ? "" : CommentServiceConst.SELECT_PRODUCT_COMMENT_WHERE_0,
                            string.IsNullOrEmpty(qTitle) ? "" : CommentServiceConst.SELECT_PRODUCT_COMMENT_WHERE_1);
                        list = _dapperRepository.Query<CommentListModel>(productSql,
                              new
                              {
                                  start = dto.start,
                                  length = dto.length,
                                  worksId = qId.Equals(Guid.Empty) ? "" : dto.q,
                                  title = string.IsNullOrEmpty(qTitle) ? "" : "%" + dto.q + "%"
                              }).ToList();

                        productCountSql = string.Format(CommentServiceConst.QUERY_PRODUCT_COMMENT_COUNT,
                            qId.Equals(Guid.Empty) ? "" : CommentServiceConst.SELECT_PRODUCT_COMMENT_WHERE_0,
                            string.IsNullOrEmpty(qTitle) ? "" : CommentServiceConst.SELECT_PRODUCT_COMMENT_WHERE_1);
                        total = _dapperRepository.ExecuteScalar<int>(productCountSql, new
                        {
                            worksId = qId.Equals(Guid.Empty) ? "" : dto.q,
                            title = string.IsNullOrEmpty(qTitle) ? "" : "%" + dto.q + "%"
                        });


                        break;
                    default:
                        throw new RequestErrorException("类型错误");
                }
                GetCommentResponseDto resp = new GetCommentResponseDto();
                resp.data = Mapper.Map<List<CommentDto>>(list);
                resp.total = total;
                return resp;


            });

        }

        public Task<DeleteCommentResponseDto> DeleteCommentByIdAsync(DeleteCommentRequestDto dto)
        {
            return Task.Run(() =>
            {
                var comment = _commentRepository.GetById(dto.Comment_id);
                var resp = new DeleteCommentResponseDto();
                if (comment != null)
                {

                    if (!comment.IsOffLine)
                    {
                        comment.IsOffLine = true;
                        _commentRepository.Update(comment);
                        _commentDeleteHistoryRepository.Insert(new CommentDeleteHistoryEntity()
                        {
                            CommentId = dto.Comment_id,
                            Reason = dto.reason,
                            GMTCreate = DateTime.Now,
                            GMTModified = null
                        });
                        resp.success = 1;
                    }
                    else
                    {
                        resp.success = 0;
                        resp.msg = "评论不可重复删除";
                    }
                }
                else
                {
                    resp.success = 0;
                    resp.msg = "评论不存在";
                }

                return resp;
            });

        }

        public Task<GetReportListResponseDto> GetReportListAsync(GetReportListRequestDto dto)
        {
            return Task.Run(() =>
            {
                var reportsList = _commentReportRepository.TableNoTracking.Where(t => t.AuditStatus.Equals(Communiry.Entity.Comment.AuditStatusEnum.pending.ToString()))
              .OrderByDescending(t => t.GMTCreate)
              .Skip(dto.start)
              .Take(dto.length).ToList();
                GetReportListResponseDto resp = new GetReportListResponseDto();
                resp.data = Mapper.Map<List<ReportDto>>(reportsList);
                resp.total =
                    _commentReportRepository.TableNoTracking.Count(
                        t => t.AuditStatus.Equals(Communiry.Entity.Comment.AuditStatusEnum.pending.ToString()));
                return resp;


            });

        }

        public Task<ReportAuditResponseDto> AuditReportAsync(ReportAuditRequestDto dto)
        {
            return Task.Run(() =>
            {
                var ids = dto.report_id.Split(',');
                var idsList = _commentReportRepository.Table.Where(t => ids.Contains(t.Id.ToString())).ToList();
                if (idsList.Any())
                {
                    switch (dto.status)
                    {
                        case AuditStatusEnum.receive:
                            foreach (CommentReportEntity entity in idsList)
                            {
                                entity.AuditStatus = AuditStatusEnum.receive.ToString();
                                entity.GMTModified = DateTime.Now;
                                var comment = _commentRepository.Table.SingleOrDefault(t => t.Id.Equals(entity.CommentId));
                                if (comment != null)
                                {
                                    comment.IsOffLine = true;
                                    comment.GMTModified = DateTime.Now;
                                    _commentRepository.Update(comment);
                                }
                            }

                            break;
                        case AuditStatusEnum.reject:
                            foreach (CommentReportEntity entity in idsList)
                            {
                                entity.AuditStatus = AuditStatusEnum.reject.ToString();
                                entity.GMTModified = DateTime.Now;

                            }
                            break;
                        default:
                            throw new RequestErrorException("类型错误");
                    }
                    _commentReportRepository.Update(idsList);
                }

                return new ReportAuditResponseDto()
                {
                    success = 0,
                    msg = ""
                };

            });

        }
        #endregion
    }
}
