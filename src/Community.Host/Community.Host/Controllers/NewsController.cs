using Community.Contact.News;
using Community.IService;
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
    public class NewsController : ApiController
    {
         private INewsService _newsService;
         public NewsController(INewsService newsService)
         {
             this._newsService = newsService;
         }
       
        /// <summary>
        ///按条件搜索新闻
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("news/search")]
        [ResponseType(typeof(GetNewsListResponse))]
         public async Task<IHttpActionResult> Get([FromUri] GetNewsList dto)
        {
            var resp = await _newsService.GetNewsListAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        ///添加新闻
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("news")]
        [ResponseType(typeof(CreateNewsResponse))]
        public async Task<IHttpActionResult> Post([FromBody] CreateNews dto)
        {
            var resp = await _newsService.CreateNewsAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        ///修改新闻热门
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [Route("news/hot")]
        [ResponseType(typeof(UpdateNewsHotStatusResponse))]
        public async Task<IHttpActionResult> Patch([FromBody] UpdateNewsHotStatus dto)
        {
            var resp = await _newsService.UpdateNewsHotStatusAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        ///修改新闻下线状态
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [Route("news/offline")]
        [ResponseType(typeof(UpdateNewsOffLineStatusResponse))]
        public async Task<IHttpActionResult> Patch([FromBody] UpdateNewsOffLineStatus dto)
        {
            var resp = await _newsService.UpdateNewsOffLineStatusAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        ///修改新闻所有属性
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("news")]
        [ResponseType(typeof(UpdateNewsResponse))]
        public async Task<IHttpActionResult> Put([FromBody] UpdateNews dto)
        {
            var resp = await _newsService.UpdateNewsAsync(dto);
            return Ok(resp);
        }
    }
}
