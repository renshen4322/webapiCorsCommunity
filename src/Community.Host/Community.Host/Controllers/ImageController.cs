using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using Community.Contact.Image;
using Community.IService;

namespace Community.Host.Controllers
{
     [RoutePrefix("v1")]
    public class ImageController : ApiController
    {
          private IImageService _imageService;

          public ImageController(IImageService imageService)
        {
            this._imageService = imageService;
        }

        /// <summary>
        /// 获取系统使用的图片列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("system/image")]
        [ResponseType(typeof(GetSystemImageListResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetSystemImageList dto)
        {
            var resp = await _imageService.GetSystemImageListAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("system/project")]
        [ResponseType(typeof(GetImageProjectNameResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetImageProjectName dto)
        {

            var resp = await _imageService.GetImageProjectNameAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 添加系统新图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("system/image")]
        [ResponseType(typeof(AddSystemImageResponse))]
        public async Task<IHttpActionResult> Get([FromBody]AddSystemImage dto)
        {

            var resp = await _imageService.AddSystemImageAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 添加项目名
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("system/project")]
        [ResponseType(typeof(AddProjectResponse))]
        public async Task<IHttpActionResult> Get([FromBody]AddProject dto)
        {

            var resp = await _imageService.AddProjectAsync(dto);
            return Ok(resp);
        }
    }
}