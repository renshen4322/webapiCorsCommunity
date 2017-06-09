using AutoMapper;
using Communiry.Entity;
using Communiry.Entity.EnumType;
using Community.Common;
using Community.Common.Exception;
using Community.Contact.Category;
using Community.Core.Data;
using Community.IService;
using Community.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Service
{
    public class CategoryService : ICategoryService
    {
        #region Fields
        private readonly IRepository<CategoryTypeConfigEntity> _categoryTypeConfigRepository;
        private readonly IRepository<CategoryEntity> _categoryEntityRepository;
        private readonly IRepository<CategoryRelationshipsEntity> _categoryRelationshipsRepository;
        private readonly IRepository<WorksEntity> _worksRepository;
        private readonly IRepository<NewsEntity> _newsRepository;
        private readonly IRepository<ProductEntity> _pruoductRepository;
        private readonly IDapperRepository _dapperRepository;

        private const string DELETE_CATEGORY_RELATIONSHIPS = "delete from community_category_relationships where object_id=@objectId;";
        #endregion

        #region Ctor
        public CategoryService(
            IRepository<WorksEntity> worksRepository,
            IRepository<CategoryTypeConfigEntity> categoryTypeConfigRepository,
                               IRepository<CategoryRelationshipsEntity> categoryRelationshipsRepository,
                               IRepository<CategoryEntity> categoryEntityRepository,
                               IRepository<ProductEntity> pruoductRepository,
            IRepository<NewsEntity> newsRepository,
        IDapperRepository dapperRepository)
        {
            this._worksRepository = worksRepository;
            this._categoryTypeConfigRepository = categoryTypeConfigRepository;
            this._categoryEntityRepository = categoryEntityRepository;
            this._categoryRelationshipsRepository = categoryRelationshipsRepository;
            this._pruoductRepository = pruoductRepository;
            this._dapperRepository = dapperRepository;
            this._newsRepository = newsRepository;
        }
        #endregion

        #region Methods

        public Task<UpdateObjectCategoryResponse> UpdateCategoryAsync(UpdateObjectCategory dto)
        {
            return Task.Run(() =>
            {
                switch (dto.type)
                {

                    case Contact.Enum.UpdateCategoryTypeEnum.Works:
                        var worksEntity = _worksRepository.TableNoTracking.Where(t => t.Id.Equals(dto.object_id)).SingleOrDefault();
                        if (worksEntity == null) throw new RequestErrorException("该作品不存在!");                       
                        break;
                    case Contact.Enum.UpdateCategoryTypeEnum.Product:
                        var productEntity = _pruoductRepository.TableNoTracking.Where(t => t.Id.Equals(dto.object_id)).SingleOrDefault();
                        if (productEntity == null) throw new RequestErrorException("该产品不存在!");
                      
                        break;
                    case Contact.Enum.UpdateCategoryTypeEnum.News:
                        var newsEntity = _newsRepository.TableNoTracking.Where(t => t.Id.Equals(dto.object_id)).SingleOrDefault();
                        if (newsEntity == null) throw new RequestErrorException("该新闻不存在!");

                        break;
                    default:
                        throw new RequestErrorException("更新的资源类型不匹配");
                        break;
                }

                var id = dto.object_id.ToString("D");
                var deleteCount = _dapperRepository.Execute(DELETE_CATEGORY_RELATIONSHIPS, new { objectId = id });
                var idList = dto.categorie_ids.Split(',');
                List<CategoryRelationshipsEntity> relaList = new List<CategoryRelationshipsEntity>();
                for (int i = 0; i < idList.Length; i++)
                {
                    relaList.Add(new CategoryRelationshipsEntity()
                    {
                        CategoryId = Convert.ToInt32(idList[i]),
                        CreatedDate = DateTime.Now,
                        ObjectId = dto.object_id
                    });
                }
                _categoryRelationshipsRepository.Insert(relaList);
                return new UpdateObjectCategoryResponse();
            });
        }

        public Task<GetTargetAllCategoryResponse> GetAllCategoryAsync(GetTargetAllCategory dto)
        {

            return Task.Run(() =>
           {
               GetTargetAllCategoryResponse resp = new GetTargetAllCategoryResponse();

               resp = new GetTargetAllCategoryResponse();
               var categoryType = _categoryTypeConfigRepository.TableNoTracking.Where(t => t.TypeName.Equals(dto.type.ToString(), StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
               if (categoryType != null)
               {
                   List<CategoryEntity> categoryList = _categoryEntityRepository.TableNoTracking.Where(t => t.TypeId.Equals(categoryType.Id)).ToList();
                   resp.category_list = CategorySort(categoryList);
               }
               else
               {
                   throw new NotFoundException(string.Format("不存在{0}类型的类目", dto.type));
               }


               return resp;

           });
        }
        public Task<CreateCategoryResponse> CreateCategoryAsync(CreateCategory dto)
        {
            return Task.Run(() =>
            {
                var categoryEntity = _categoryEntityRepository.Table.Where(t => t.TypeId.Equals(dto.type_id) && t.Id.Equals(dto.parent_id)).SingleOrDefault();
                if (categoryEntity == null) throw new RequestErrorException("父级分类不存在");
                CategoryEntity entity = new CategoryEntity();
                entity.TypeId = dto.type_id;
                entity.ParentId = dto.parent_id;
                entity.Name = dto.cn_name;
                entity.OffLine = dto.off_line;
                entity.IsHot = dto.is_hot;
                entity.DisplayIndex = dto.displayindex;
                entity.CreatedDate = DateTime.Now;
                entity.IsMultiple = false;
                entity.EnName = dto.en_name;
                entity.SysName = dto.en_name;
                entity.IsSystemCategory = false;
                _categoryEntityRepository.Insert(entity);
                return new CreateCategoryResponse() { id=entity.Id};
            });
        }

        public Task<UpdateCategoryDisplayIndexResponse> UpdateDisplayIndexAsync(UpdateCategoryDisplayIndex dto)
        {

            return Task.Run(() =>
            {
                var categoryEntity = _categoryEntityRepository.Table.Where(t => t.Id.Equals(dto.id)).SingleOrDefault();
                if (categoryEntity == null) throw new RequestErrorException("分类不存在");
                categoryEntity.DisplayIndex = dto.display_index;
                _categoryEntityRepository.Update(categoryEntity);
                return new UpdateCategoryDisplayIndexResponse();
            });
        }

        public Task<UpdateCategoryHotStatusResponse> UpdateHotStatusAsync(UpdateCategoryHotStatus dto)
        {
            return Task.Run(() =>
            {
                var categoryEntity = _categoryEntityRepository.Table.Where(t => t.Id.Equals(dto.id)).SingleOrDefault();
                if (categoryEntity == null) throw new RequestErrorException("分类不存在");
                categoryEntity.IsHot = dto.is_hot;
                _categoryEntityRepository.Update(categoryEntity);
                return new UpdateCategoryHotStatusResponse();
            });
        }

        public Task<UpdateCategoryOffLineStatusResponse> UpdateOffLineStatusAsync(UpdateCategoryOffLineStatus dto)
        {
            return Task.Run(() =>
            {
                var categoryEntity = _categoryEntityRepository.Table.Where(t => t.Id.Equals(dto.id)).SingleOrDefault();
                if (categoryEntity == null) throw new RequestErrorException("分类不存在");
                categoryEntity.OffLine = dto.off_line;
                _categoryEntityRepository.Update(categoryEntity);
                return new UpdateCategoryOffLineStatusResponse();
            });
        }

        #endregion

        #region Utilities
        /// <summary>
        /// 目录排序
        /// </summary>
        /// <param name="entiyList"></param>
        /// <param name="categoryList"></param>
        private List<CategoryList> CategorySort(List<CategoryEntity> entiyList)
        {
            List<CategoryList> categoryList = new List<CategoryList>();
            foreach (var item in entiyList)
            {
                if (item.ParentId.Equals(0))
                {
                    var category = new CategoryList();
                    category.type_id = item.Id;
                    category.type = item.Name;
                  
                    category.is_multiple = item.IsMultiple;
                    categoryList.Add(category);
                }
            }
            foreach (var item in entiyList)
            {
                foreach (var category in categoryList)
                {
                    if (item.ParentId.Equals(category.type_id))
                    {
                        category.list.Add(new Category() 
                        { id = item.Id,
                            cn_name = item.Name,
                            is_hot=item.IsHot,
                            off_line=item.OffLine,
                            display_index=item.DisplayIndex,
                            en_name=item.EnName
                        });
                    }
                }
            }
            return categoryList;
        }


        #endregion









    }
}
