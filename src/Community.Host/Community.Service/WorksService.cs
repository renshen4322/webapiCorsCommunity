using AutoMapper;
using Communiry.Entity;
using Communiry.Entity.EnumType;
using Community.Common;
using Community.Common.Exception;
using Community.Contact.Works;
using Community.Contact.Works.Component;
using Community.Core.Data;
using Community.IService;
using Community.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service
{
    public class WorksService : IWorksService
    {
        #region Fields
        private readonly IRepository<WorksEntity> _worksRepository;
        private readonly IRepository<WorksItemsEntity> _worksItemsRepository;
        private readonly IRepository<WorksMetaEntity> _worksMetaRepository;
        private readonly IRepository<CategoryRelationshipsEntity> _categoryRelationshipsRepository;
        private readonly IRepository<BaseUserEntity> _baseUserRepository;
        private readonly IDapperRepository _dapperRepository;
        #region sql
        #region 按条件查询产品

        private const string SELECT_WORKS_COUNT = "select count(1) ";

        private const string SELECT_WORKS_SELECT = " select cbu.nick_name as 'Owner',cbu.id as 'OwnerId', cw.id as 'WorksId', cw.name as 'WorksName',cw.thumbnail as 'Thumbnail', "
                                                    +"cw.introduction as 'Introduction', cw.off_line as 'OffLine',cwm.is_hot as 'IsHot',cw.created_date as 'CreatedDate' ";

        private const string SELECT_WORKS_FROM = "{0} from community_works as cw "
                                                    + "inner JOIN community_base_user as cbu "
                                                    + "on cw.user_id=cbu.id "
                                                    + "inner JOIN community_works_meta as cwm "
                                                    + "on cwm.works_id=cw.id"
                                                    + " {1} "
                                                 + "ORDER BY cw.created_date desc {2} ;";//
        private const string SELECT_WORKS_WHERE_CAT = "cw.id in (select object_id from community_category_relationships "
                                                + "where ({0}) "
                                                + "GROUP BY object_id  "
                                                + "HAVING count(object_id)>{1}) ";
        private const string SELECT_WORKS_WHERE_HOT = " is_hot={0} ";
        private const string SELECT_WORKS_WHERE_OFFLINE = " off_line={0} ";
        private const string SELECT_WORKS_WHERE_USER = " (cbu.id='{0}' or cbu.nick_name like '%{0}%' or cw.id='{0}' or cw.`name` like '%{0}%') ";


        #endregion 
        #endregion
        #endregion

        #region Ctor
        public WorksService(IRepository<WorksEntity> worksRepository,
                IRepository<WorksItemsEntity> worksItemsRepository,
                IRepository<WorksMetaEntity> worksMetaRepository,
                IRepository<CategoryRelationshipsEntity> categoryRelationshipsRepository,
                IRepository<BaseUserEntity> baseUserRepository,
                IDapperRepository dapperRepository)
        {
            this._worksItemsRepository = worksItemsRepository;
            this._worksMetaRepository = worksMetaRepository;
            this._worksRepository = worksRepository;
            this._categoryRelationshipsRepository = categoryRelationshipsRepository;
            this._baseUserRepository = baseUserRepository;
            this._dapperRepository = dapperRepository;
        }
        #endregion

        #region Method

        public Task<GetWorksListResponse> GetListAsync(GetWorksList dto)
        {
            return Task.Run(() =>
            {
                string sqlWhere = "";
                switch (dto.type)
                {
                    case Community.Contact.Enum.HotEnum.All:
                        break;
                    case Community.Contact.Enum.HotEnum.Hot:
                        sqlWhere += string.Format(SELECT_WORKS_WHERE_HOT, 1);
                        break;
                    case Community.Contact.Enum.HotEnum.UnHot:
                        sqlWhere += string.Format(SELECT_WORKS_WHERE_HOT, 0);
                        break;
                    default:
                        break;
                }
                switch (dto.off_line)
                {
                    case Community.Contact.Enum.OffLineEnum.All:
                        break;
                    case Community.Contact.Enum.OffLineEnum.OffLine:
                        if (sqlWhere.Length < 1)
                        {
                            sqlWhere += string.Format(SELECT_WORKS_WHERE_OFFLINE, 1);
                        }
                        else
                        {
                            sqlWhere += " and ";
                            sqlWhere += string.Format(SELECT_WORKS_WHERE_OFFLINE, 1);

                        }
                        break;
                    case Community.Contact.Enum.OffLineEnum.UnOffLine:
                        if (sqlWhere.Length < 1)
                        {
                            sqlWhere += string.Format(SELECT_WORKS_WHERE_OFFLINE, 0);
                        }
                        else
                        {
                            sqlWhere += " and ";
                            sqlWhere += string.Format(SELECT_WORKS_WHERE_OFFLINE, 0);

                        }
                        break;
                    default:
                        break;
                }
                if (sqlWhere.Length > 1)
                {
                    sqlWhere = " where " + sqlWhere;
                }
                if (!string.IsNullOrEmpty(dto.q))
                {
                    sqlWhere = sqlWhere + " and " + string.Format(SELECT_WORKS_WHERE_USER, dto.q);
                }
                List<SearchWorksModel> data = new List<SearchWorksModel>();
                int count = 0;
                string sqlData = "";
                string sqlCount = "";
                if (string.IsNullOrEmpty(dto.category_id))
                {
                    sqlData = string.Format(SELECT_WORKS_FROM, SELECT_WORKS_SELECT, sqlWhere, string.Format("LIMIT {0},{1}", dto.start, dto.length));
                    sqlCount = string.Format(SELECT_WORKS_FROM, SELECT_WORKS_COUNT, sqlWhere, "");
                }
                else
                {

                    string[] typeidList = dto.category_id.Split(',');
                    var builder = new StringBuilder();
                    for (int i = 0; i < typeidList.Length; i++)
                    {
                        builder.Append("category_id= " + typeidList[i]);
                        if (i != typeidList.Length - 1)
                        {
                            builder.Append("||");
                        }
                    }
                    if (sqlWhere.Length > 0)
                    {
                        sqlWhere = sqlWhere + " and " + string.Format(SELECT_WORKS_WHERE_CAT, builder.ToString(), typeidList.Count() - 1);


                    }
                    else
                    {
                        sqlWhere = " where " + string.Format(SELECT_WORKS_WHERE_CAT, builder.ToString(), typeidList.Count() - 1);
                    }
                    sqlData = string.Format(SELECT_WORKS_FROM, SELECT_WORKS_SELECT, sqlWhere, string.Format("LIMIT {0},{1}", dto.start, dto.length));

                    sqlCount = string.Format(SELECT_WORKS_FROM, SELECT_WORKS_COUNT, sqlWhere, "");
                }
                data = _dapperRepository.Query<SearchWorksModel>(sqlData).ToList();
                if (data != null && data.Count() > 0)
                {
                    count = _dapperRepository.QuerySingleOrDefault<int>(sqlCount);
                }
                GetWorksListResponse resp = new GetWorksListResponse();
                resp.data = Mapper.Map<List<WorksInfo>>(data);
                resp.total = count;
                return resp;
            });
        }

        public Task<GetWorksDetailResponse> GetDetailAsync(GetWorksDetail dto)
        {
            return Task.Run(() =>
            {
                var worksEntity = _worksRepository.TableNoTracking.Where(t => t.Id.Equals(dto.id)).SingleOrDefault();
                if (worksEntity != null)
                {
                    var metaData = _worksMetaRepository.TableNoTracking.Where(t => t.WorksId.Equals(worksEntity.Id)).SingleOrDefault();
                    var itemList = _worksItemsRepository.TableNoTracking.Where(t => t.WorksId.Equals(worksEntity.Id)).ToList();
                    GetWorksDetailResponse resp = Mapper.Map<GetWorksDetailResponse>(worksEntity);
                    if (metaData != null)
                    {
                        resp.actual_area = metaData.ActualArea;
                        resp.cost = metaData.Cost;
                        resp.helper = metaData.helper;
                        resp.is_hot = metaData.IsHot;
                    }
                    if (itemList != null && itemList.Count() > 0)
                    {
                        Mapper.Map(itemList, resp.products);
                        resp.products = Mapper.Map<List<ItemInfo>>(itemList);
                    }
                    return resp;
                }
                else
                {
                    throw new NotFoundException(string.Format("不存在id为:{0}的作品!", dto.id.ToString()));
                }
            });
        }

   
        public Task<UpdateWorksHotStatusResponse> UpdateHotStatusAsync(UpdateWorksHotStatus dto)
        {
            return Task.Run(() =>
            {
                var worksEntity = _worksRepository.TableNoTracking.Where(t => t.Id.Equals(dto.works_id)).SingleOrDefault();
                if (worksEntity == null) throw new RequestErrorException("该作品不存在!");
                var worksMeta = _worksMetaRepository.Table.Where(t => t.WorksId.Equals(dto.works_id)).SingleOrDefault();

                worksMeta.IsHot = dto.is_hot;
                _worksMetaRepository.Update(worksMeta);
                return new UpdateWorksHotStatusResponse();

            });
        }

        public Task<UpdateWorksOffLineStatusResponse> UpdateOffLineStatusAsync(UpdateWorksOffLineStatus dto)
        {
            return Task.Run(() =>
            {
                var worksEntity = _worksRepository.Table.Where(t => t.Id.Equals(dto.works_id)).SingleOrDefault();
                if (worksEntity == null) throw new RequestErrorException("该作品不存在!");

                worksEntity.OffLine = dto.off_line;
                _worksRepository.Update(worksEntity);
                return new UpdateWorksOffLineStatusResponse();

            });
        }
        #endregion

        #region Utilities
        public List<DesignerWorksListModel> CombineWorksByStyle(List<DesignerWorksListModel> data)
        {
            List<DesignerWorksListModel> resu = new List<DesignerWorksListModel>();
            resu = (from n in data.Distinct(new DesignerWorksListModel())
                    select new DesignerWorksListModel
                    {
                        CreatedDate = n.CreatedDate,
                        Style = "",
                        WorksId = n.WorksId,
                        OwnerId = n.OwnerId,
                        DesignDate = n.DesignDate,
                        Images = n.Images,
                        ImageThumbnail = n.ImageThumbnail,
                        Introduction = n.Introduction,
                        Name = n.Name,
                        PanoThumbnail = n.PanoThumbnail,
                        PanoUrl = n.PanoUrl
                    }
                       ).ToList();
            foreach (var destItem in resu)
            {
                foreach (var originItem in data)
                {
                    if (destItem.WorksId.Equals(originItem.WorksId))
                    {
                        destItem.Style += originItem.Style + ",";
                    }
                }
                destItem.Style=destItem.Style.Trim(new char[] { ',' });
            }
            return resu;
        }
        #endregion

     
    }
}
