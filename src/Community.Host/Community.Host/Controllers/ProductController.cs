using Community.Contact;
using Community.Contact.Product;
using Community.IService;
using Community.OAuth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Community.Host.Controllers
{
    [RoutePrefix("v1")]
    public class ProductController : ApiController
    {
          private IProductService _productService;
          public ProductController(IProductService productService)
         {
             this._productService = productService;
         }

       
        /// <summary>
        /// 按条件搜索产品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("product/search")]
        [ResponseType(typeof(GetProductListResponse))]
          public async Task<IHttpActionResult> Get([FromUri]GetProductList dto)
        {
            var resp = _productService.GetListAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 获取产品详情
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("product/detail")]
        [ResponseType(typeof(GetProductDetailByIdResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetProductDetailById dto)
        {
            var resp = _productService.GetDetailAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 更新产品热门状态
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [Route("product/hot")]
        [ResponseType(typeof(UpdateProductHotStatusResponse))]
        public async Task<IHttpActionResult> Patch([FromBody]UpdateProductHotStatus dto)
        {

            var resp = _productService.UpdateHotStatusAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 更新产品下线状态 
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [Route("product/offline")]
        [ResponseType(typeof(UpdateProductOffLineStatusResponse))]
        public async Task<IHttpActionResult> Patch([FromBody]UpdateProductOffLineStatus dto)
        {
            var resp = _productService.UpdateOffLineStatusAsync(dto);
            return Ok(resp);
        }
    }
}
