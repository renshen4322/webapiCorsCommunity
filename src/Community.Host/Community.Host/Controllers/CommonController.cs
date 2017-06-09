using Community.Contact.Common;
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
    public class CommonController : ApiController
    {
        private ICommonService _commonService;

        public CommonController(ICommonService commonService)
        {
            this._commonService = commonService;
        }
        /// <summary>
        /// 获取所有省份
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("province")]
        [ResponseType(typeof(List<GetProvinceResponse>))]
        public async Task<IHttpActionResult> Get([FromUri]GetProvince dto)
        {
            var resp = await _commonService.GetProvinceAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 根据省份获取所有城市
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("city")]
        [ResponseType(typeof(List<GetCityResponse>))]
        public async Task<IHttpActionResult> Get([FromUri]GetCity dto)
        {
            var resp = await _commonService.GetCityAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 根据城市获取所有区县
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("district")]
        [ResponseType(typeof(List<GetDistrictResponse>))]
        public async Task<IHttpActionResult> Get([FromUri]GetDistrict dto)
        {
            var resp = await _commonService.GetDistrictAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 获取系统资源分类数组
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("system/resource")]
        [ResponseType(typeof(GetResourceTypeListResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetResourceTypeList dto)
        {
            var resp = await _commonService.GetResourceTypeListAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 获取七牛云图片上传的上传凭证
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("qiniu/token")]
        [ResponseType(typeof(GetUploadTokenToQiniuResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetUploadTokenToQiniu dto)
        {
            var resp = await _commonService.GetUploadTokenToQiniuAsync(dto);
            return Ok(resp);
        }

      
    }
}
