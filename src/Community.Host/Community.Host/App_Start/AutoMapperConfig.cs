using AutoMapper;
using Communiry.Entity;
using Communiry.Entity.Comment;
using Communiry.Entity.Group;
using Community.Common;
using Community.Contact.Common;
using Community.Contact.Common.Component;
using Community.Contact.News;
using Community.Contact.News.Component;
using Community.Contact.Product;
using Community.Contact.Works;
using Community.Contact.Works.Component;
using Community.Service.Model;
using Community.Contact.Comment;
using Community.Contact.Group;
using Community.Contact.Image;
using Community.Service.Model.Comment;

namespace Community.Host.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                #region category
                cfg.CreateMap<CategoryTypeConfigEntity, Resource>();
                #endregion
                #region news
                cfg.CreateMap<SearchNewsModel, NewsIntro>()
                .ForMember(dest => dest.created_at, opt => opt.Ignore())
             .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.CreatedDate));
                #endregion
                #region Common

                cfg.CreateMap<ProvinceEntity, GetProvinceResponse>();
                cfg.CreateMap<DistrictEntity, GetDistrictResponse>();
                cfg.CreateMap<CityEntity, GetCityResponse>();
                #endregion
                #region Product
                cfg.CreateMap<SearchProductInfoModel, ProductInfo>()
              .ForMember(dest => dest.created_at, opt => opt.Ignore())
           .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.CreatedDate));
                cfg.CreateMap<ProductEntity, GetProductDetailByIdResponse>()
                     .ForMember(dto => dto.owner_id, (entity) => entity.MapFrom(m => m.UserId))
                  .ForMember(dto => dto.origin_id, (entity) => entity.MapFrom(m => m.OriginId))
                   .ForMember(dto => dto.is_hot, (opt) => opt.Ignore())
                  .ForMember(dto => dto.images_thumbnail, (entity) => entity.MapFrom(m => m.ImageThumbnail))
                    .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.CreatedDate));
                #endregion

                #region works
                cfg.CreateMap<SearchWorksModel, WorksInfo>()
            .ForMember(dest => dest.created_at, opt => opt.Ignore())
         .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.CreatedDate));
                cfg.CreateMap<WorksEntity, GetWorksDetailResponse>()
                .ForMember(dest => dest.owner_id, (entity) => entity.MapFrom(m => m.UserId))
                  .ForMember(dest => dest.design_date, opt => opt.Ignore())
                   .ForMember(dest => dest.created_at, opt => opt.Ignore())
                    .ForMember(dest => dest.products, opt => opt.Ignore())
               .AfterMap((src, dest) => dest.design_date = DateTimeHelper.DateTimeToStamp(src.DesignDate))
               .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.CreatedDate));
                cfg.CreateMap<WorksItemsEntity, ItemInfo>()
            .ForMember(dto => dto.owner_id, (entity) => entity.MapFrom(m => m.OwierOriginId))
             .ForMember(dto => dto.product_id, (entity) => entity.MapFrom(m => m.ProductId))
              .ForMember(dto => dto.product_url, (entity) => entity.MapFrom(m => m.ImgUrl))
               .ForMember(dto => dto.product_name, (entity) => entity.MapFrom(m => m.Name))
                .ForMember(dto => dto.works_id, (entity) => entity.MapFrom(m => m.WorksId))
                 .ForMember(dto => dto.id, (entity) => entity.MapFrom(m => m.Id));

                #endregion

                #region Image
                cfg.CreateMap<SelectProjectImageModel, SystemImageDto>()
               .ForMember(dest => dest.created_at, opt => opt.Ignore())
            .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.created_date));
                cfg.CreateMap<SysProjectNameEntity, Project>()
              .ForMember(dest => dest.id, (entity) => entity.MapFrom(m => m.Id))
           .ForMember(dest => dest.name, (entity) => entity.MapFrom(m => m.Name));
                #endregion


                #region Comment
                cfg.CreateMap<CommentListModel, CommentDto>()
                     .ForMember(dest => dest.created_at, opt => opt.Ignore())
                     .ForMember(dest => dest.id, (entity) => entity.MapFrom(m => m.Id))
                     .ForMember(dest => dest.title, (entity) => entity.MapFrom(m => m.Title))
                     .ForMember(dest => dest.author, (entity) => entity.MapFrom(m => m.Author))
                       .ForMember(dest => dest.comment_id, (entity) => entity.MapFrom(m => m.CommentId))
                     .ForMember(dest => dest.content, (entity) => entity.MapFrom(m => m.Content))
                     .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.CreatedAt));
                cfg.CreateMap<CommentReportEntity, ReportDto>()
                 .ForMember(dest => dest.created_at, opt => opt.Ignore())
                     .ForMember(dest => dest.comment_id, (entity) => entity.MapFrom(m => m.CommentId))
                     .ForMember(dest => dest.report_id, (entity) => entity.MapFrom(m => m.Id))
                     .ForMember(dest => dest.comment_content, (entity) => entity.MapFrom(m => m.CommentContent))
                     .ForMember(dest => dest.author_id, (entity) => entity.MapFrom(m => m.ReportAuthorId))
                     .ForMember(dest => dest.author_name, (entity) => entity.MapFrom(m => m.ReportAuthorName))
                     .ForMember(dest => dest.report_reason, (entity) => entity.MapFrom(m => m.ReportReason))
                     .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.GMTCreate));

                #endregion


                #region Group
                cfg.CreateMap<GroupClassifyEntity, Classify>()
                  .ForMember(dest => dest.created_at, opt => opt.Ignore())
                  .ForMember(dest => dest.id, (entity) => entity.MapFrom(m => m.Id))
                  .ForMember(dest => dest.description, (entity) => entity.MapFrom(m => m.Description))
                  .ForMember(dest => dest.order, (entity) => entity.MapFrom(m => m.Order))
                  .ForMember(dest => dest.name, (entity) => entity.MapFrom(m => m.Name))
                  .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.GMTCreate));
                cfg.CreateMap<GroupInfoEntity, GroupDetailInfo>()
                       .ForMember(dest => dest.created_at, opt => opt.Ignore())
                       .ForMember(dest => dest.id, (entity) => entity.MapFrom(m => m.Id))
                       .ForMember(dest => dest.description, (entity) => entity.MapFrom(m => m.Description))
                       .ForMember(dest => dest.cover_url, (entity) => entity.MapFrom(m => m.CoverUrl))
                       .ForMember(dest => dest.name, (entity) => entity.MapFrom(m => m.Name))
                       .ForMember(dest => dest.is_hot, (entity) => entity.MapFrom(m => m.IsHot))
                       .ForMember(dest => dest.classify_id, (entity) => entity.MapFrom(m => m.ClassifyId))
                       .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.GMTCreate));
                cfg.CreateMap<GroupPostEntity, GetPostDetailResponseDto>()
                      .ForMember(dest => dest.created_at, opt => opt.Ignore())
                      .ForMember(dest => dest.post_id, (entity) => entity.MapFrom(m => m.Id))
                      .ForMember(dest => dest.like_count, (entity) => entity.MapFrom(m => m.LikeCount))
                      .ForMember(dest => dest.commment_count, (entity) => entity.MapFrom(m => m.CommentCount))
                      .ForMember(dest => dest.collect_count, (entity) => entity.MapFrom(m => m.CollectCount))
                      .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.GMTCreate));
                cfg.CreateMap<GroupPostEntity, PostInfo>()
                     .ForMember(dest => dest.created_at, opt => opt.Ignore())
                     .ForMember(dest => dest.post_id, (entity) => entity.MapFrom(m => m.Id))
                     .ForMember(dest => dest.group_id, (entity) => entity.MapFrom(m => m.GroupId))
                     .ForMember(dest => dest.title, (entity) => entity.MapFrom(m => m.Title))
                     .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.GMTCreate));

                #endregion
            });

        }
    }
}