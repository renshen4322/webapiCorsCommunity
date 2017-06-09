using AutoMapper;
using Communiry.Entity;
using Communiry.Entity.EnumType;
using Community.Common;
using Community.Common.Exception;
using Community.Contact.Product;
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
    public class ProductService : IProductService
    {
        #region Fields
        private readonly IRepository<ProductEntity> _pruoductRepository;
        private readonly IRepository<ProductMetaEntity> _pruoductMetaRepository;
        private readonly IRepository<CategoryRelationshipsEntity> _categoryRelationshipsRepository;
        private readonly IRepository<BaseUserEntity> _baseUserRepository;
        private readonly IDapperRepository _dapperRepository;
        #region sql
        #region 按条件查询产品

        private const string SELECT_PRODUCT_COUNT = "select count(1) ";

        private const string SELECT_PRODUCT_SELECT = "select cbu.nick_name as 'Owner',cbu.id as 'OwnerId', cp.id as 'ProductId', cp.name as 'ProductName',cp.thumbnail as 'Thumbnail',"
                                                    +" cp.introduction as 'Introduction', cp.off_line as 'OffLine',cpm.is_hot as 'IsHot',cp.created_date as 'CreatedDate' ";

        private const string SELECT_PRODUCT_FROM = "{0} from community_product as cp "
                                                    +"inner JOIN community_base_user as cbu "
                                                    +"on cp.user_id=cbu.id "
                                                    +"inner JOIN community_product_meta as cpm "
                                                    +"on cpm.product_id=cp.id"
                                                    + " {1} "
                                                 + "ORDER BY cp.created_date desc {2} ;";//
        private const string SELECT_PRODUCT_WHERE_CAT = "cp.id in (select object_id from community_category_relationships "
                                                + "where ({0}) "
                                                + "GROUP BY object_id  "
                                                + "HAVING count(object_id)>{1}) ";
        private const string SELECT_PRODUCT_WHERE_HOT = " is_hot={0} ";
        private const string SELECT_PRODUCT_WHERE_OFFLINE = " off_line={0} ";
        private const string SELECT_PRODUCT_WHERE_USER = " (cbu.id='{0}' or cbu.nick_name like '%{0}%' or cp.id='{0}' or cp.`name` like '%{0}%') ";


        #endregion 
        #endregion
        #endregion


        #region Ctor
        public ProductService(IRepository<ProductEntity> pruoductRepository,
                              IRepository<ProductMetaEntity> pruoductMetaRepository,
                              IRepository<CategoryRelationshipsEntity> categoryRelationshipsRepository,
                              IRepository<BaseUserEntity> baseUserRepository,
                              IDapperRepository dapperRepository)
        {
            this._pruoductRepository = pruoductRepository;
            this._pruoductMetaRepository = pruoductMetaRepository;
            this._categoryRelationshipsRepository = categoryRelationshipsRepository;
            this._baseUserRepository = baseUserRepository;
            this._dapperRepository = dapperRepository;

        }

        #endregion

        #region Method
        public Task<GetProductListResponse> GetListAsync(GetProductList dto)
        {
            return Task.Run(() =>
            {
                string sqlWhere = "";
                switch (dto.type)
                {
                    case Community.Contact.Enum.HotEnum.All:
                        break;
                    case Community.Contact.Enum.HotEnum.Hot:
                        sqlWhere += string.Format(SELECT_PRODUCT_WHERE_HOT, 1);
                        break;
                    case Community.Contact.Enum.HotEnum.UnHot:
                        sqlWhere += string.Format(SELECT_PRODUCT_WHERE_HOT, 0);
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
                            sqlWhere += string.Format(SELECT_PRODUCT_WHERE_OFFLINE, 1);
                        }
                        else
                        {
                            sqlWhere += " and ";
                            sqlWhere += string.Format(SELECT_PRODUCT_WHERE_OFFLINE, 1);

                        }
                        break;
                    case Community.Contact.Enum.OffLineEnum.UnOffLine:
                        if (sqlWhere.Length < 1)
                        {
                            sqlWhere += string.Format(SELECT_PRODUCT_WHERE_OFFLINE, 0);
                        }
                        else
                        {
                            sqlWhere += " and ";
                            sqlWhere += string.Format(SELECT_PRODUCT_WHERE_OFFLINE, 0);

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
                    sqlWhere = sqlWhere + " and " + string.Format(SELECT_PRODUCT_WHERE_USER, dto.q);
                }
                List<SearchProductInfoModel> data = new List<SearchProductInfoModel>();
                int count = 0;
                string sqlData = "";
                string sqlCount = "";
                if (string.IsNullOrEmpty(dto.category_id))
                {
                    sqlData = string.Format(SELECT_PRODUCT_FROM, SELECT_PRODUCT_SELECT, sqlWhere, string.Format("LIMIT {0},{1}", dto.start, dto.length));
                    sqlCount = string.Format(SELECT_PRODUCT_FROM, SELECT_PRODUCT_COUNT, sqlWhere, "");
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
                        sqlWhere = sqlWhere + " and " + string.Format(SELECT_PRODUCT_WHERE_CAT, builder.ToString(), typeidList.Count() - 1);


                    }
                    else
                    {
                        sqlWhere = " where " + string.Format(SELECT_PRODUCT_WHERE_CAT, builder.ToString(), typeidList.Count() - 1);
                    }
                    sqlData = string.Format(SELECT_PRODUCT_FROM, SELECT_PRODUCT_SELECT, sqlWhere, string.Format("LIMIT {0},{1}", dto.start, dto.length));

                    sqlCount = string.Format(SELECT_PRODUCT_FROM, SELECT_PRODUCT_COUNT, sqlWhere, "");
                }
                data = _dapperRepository.Query<SearchProductInfoModel>(sqlData).ToList();
                if (data != null && data.Count() > 0)
                {
                    count = _dapperRepository.QuerySingleOrDefault<int>(sqlCount);
                }
                GetProductListResponse resp = new GetProductListResponse();
                resp.data = Mapper.Map<List<ProductInfo>>(data);
                resp.total = count;
                return resp;
            });
        }

        public Task<GetProductDetailByIdResponse> GetDetailAsync(GetProductDetailById dto)
        {
            return Task.Run(() =>
            {
                var worksEntity = _pruoductRepository.TableNoTracking.Where(t => t.Id.Equals(dto.id)).SingleOrDefault();
                if (worksEntity != null)
                {

                    GetProductDetailByIdResponse resp = Mapper.Map<GetProductDetailByIdResponse>(worksEntity);
                   var productMeta= _pruoductMetaRepository.TableNoTracking.Where(t => t.PruductId.Equals(dto.id)).SingleOrDefault();
                   resp.is_hot = productMeta.IsHot;
                    return resp;
                }
                else
                {
                    throw new NotFoundException(string.Format("不存在id为:{0}的产品!", dto.id.ToString()));
                }
            });
        }

      
        public Task<UpdateProductHotStatusResponse> UpdateHotStatusAsync(UpdateProductHotStatus dto)
        {
            return Task.Run(() =>
            {
                var pruoductEntity = _pruoductRepository.Table.Where(t => t.Id.Equals(dto.product_id)).SingleOrDefault();
                if (pruoductEntity == null) throw new RequestErrorException("该产品不存在!");
                var productMeta = _pruoductMetaRepository.Table.Where(t => t.PruductId.Equals(dto.product_id)).SingleOrDefault();

                productMeta.IsHot = dto.is_hot;
                _pruoductMetaRepository.Update(productMeta);
                return new UpdateProductHotStatusResponse();

            });
        }

        public Task<UpdateProductOffLineStatusResponse> UpdateOffLineStatusAsync(UpdateProductOffLineStatus dto)
        {
            return Task.Run(() =>
            {
                var pruoductEntity = _pruoductRepository.Table.Where(t => t.Id.Equals(dto.product_id)).SingleOrDefault();
                if (pruoductEntity == null) throw new RequestErrorException("该产品不存在!");

                pruoductEntity.OffLine = dto.off_line;
                _pruoductRepository.Update(pruoductEntity);
                return new UpdateProductOffLineStatusResponse();

            });
        }
        #endregion

        #region Utilities
        #endregion

     
    }
}
