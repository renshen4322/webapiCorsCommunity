using Community.Contact.Category;
using Community.IService;
using Community.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Community.Host.Controllers
{
    [RoutePrefix("v1")]
    public class CategoryController : ApiController
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        
        /// <summary>
        ///获取类型所有分类信息
        /// </summary>
        ///<remarks>
        /// 根据type获取所有分类类目
        /// News:新闻
        /// Product:产品
        /// Works:作品
        /// </remarks>
        /// <returns></returns>        
        [HttpGet]
        [Route("category/search")]
        [ResponseType(typeof(GetTargetAllCategoryResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetTargetAllCategory dto)
        {
           var resp=await _categoryService.GetAllCategoryAsync(dto);
           return Ok(resp);
        }
        /// <summary>
        ///更新对象分类信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("category/object")]
        [ResponseType(typeof(UpdateObjectCategoryResponse))]
        public async Task<IHttpActionResult> Post([FromBody] UpdateObjectCategory dto)
        {
           
            var resp = await _categoryService.UpdateCategoryAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        ///更新分类显示顺序
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [Route("category/displayindex")]
        [ResponseType(typeof(UpdateCategoryDisplayIndexResponse))]
        public async Task<IHttpActionResult> Patch([FromBody] UpdateCategoryDisplayIndex dto)
        {
            var resp = await _categoryService.UpdateDisplayIndexAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        ///更新热门分类
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [Route("category/hot")]
        [ResponseType(typeof(UpdateCategoryHotStatusResponse))]
        public async Task<IHttpActionResult> Patch([FromBody] UpdateCategoryHotStatus dto)
        {

            var resp = await _categoryService.UpdateHotStatusAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        ///更新分类下线状态
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [Route("category/offline")]
        [ResponseType(typeof(UpdateCategoryOffLineStatusResponse))]
        public async Task<IHttpActionResult> Patch([FromBody] UpdateCategoryOffLineStatus dto)
        {
            var resp = await _categoryService.UpdateOffLineStatusAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        ///添加新分类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("category")]
        [ResponseType(typeof(CreateCategoryResponse))]
        public async Task<IHttpActionResult> Post([FromBody]CreateCategory dto)
        {

            var resp = await _categoryService.CreateCategoryAsync(dto);
            return Ok(resp);
        }
        
    }
}
