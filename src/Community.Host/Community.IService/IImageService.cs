using Community.Contact.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IService
{
   public interface IImageService
    {
       Task<GetSystemImageListResponse> GetSystemImageListAsync(GetSystemImageList dto);
       Task<GetImageProjectNameResponse> GetImageProjectNameAsync(GetImageProjectName dto);
       Task<AddSystemImageResponse> AddSystemImageAsync(AddSystemImage dto);
       Task<AddProjectResponse> AddProjectAsync(AddProject dto);
    }
}
