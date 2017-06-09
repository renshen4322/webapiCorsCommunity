using Community.Contact.Works;
using Community.IService;
using Community.OAuth;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace Community.Host.Controllers
{
     [RoutePrefix("v1")]
    public class WorksController : ApiController
    {
         private IWorksService _worksService;
         public WorksController(IWorksService worksService)
         {
             this._worksService = worksService;
         }
         /// <summary>
         /// 按条件搜索作品
         /// </summary>
         /// <param name="dto"></param>
         /// <returns></returns>
         [HttpGet]
         [Route("works/search")]
         [ResponseType(typeof(GetWorksListResponse))]
         public async Task<IHttpActionResult> Get([FromUri]GetWorksList dto)
         {
             var resp = await _worksService.GetListAsync(dto);
             return Ok(resp);
         }
         /// <summary>
         /// 获取作品详情
         /// </summary>
         /// <param name="dto"></param>
         /// <returns></returns>
         [HttpGet]
         [Route("works/detail")]
         [ResponseType(typeof(GetWorksDetailResponse))]
         public async Task<IHttpActionResult> Get([FromUri]GetWorksDetail dto)
         {
             var resp = await _worksService.GetDetailAsync(dto);
             return Ok(resp);
         }
         /// <summary>
         /// 更新作品热门状态
         /// </summary>
         /// <returns></returns>
         [HttpPatch]
         [Route("works/hot")]
         [ResponseType(typeof(UpdateWorksHotStatusResponse))]
         public async Task<IHttpActionResult> Patch([FromBody]UpdateWorksHotStatus dto)
         {

             var resp = await _worksService.UpdateHotStatusAsync(dto);
             return Ok(resp);
         }
         /// <summary>
         /// 更新作品下线状态 
         /// </summary>
         /// <returns></returns>
         [HttpPatch]
         [Route("works/offline")]
         [ResponseType(typeof(UpdateWorksOffLineStatusResponse))]
         public async Task<IHttpActionResult> Patch([FromBody]UpdateWorksOffLineStatus dto)
         {
             var resp = await _worksService.UpdateOffLineStatusAsync(dto);
             return Ok(resp);
         }


    }
}
