using Community.Contact.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IService
{
   public interface ICommonService
    {
       Task<List<GetProvinceResponse>> GetProvinceAsync(GetProvince dto);
       Task<List<GetCityResponse>> GetCityAsync(GetCity dto);
       Task<List<GetDistrictResponse>> GetDistrictAsync(GetDistrict dto);
       Task<GetResourceTypeListResponse> GetResourceTypeListAsync(GetResourceTypeList dto);

       Task<GetUploadTokenToQiniuResponse> GetUploadTokenToQiniuAsync(GetUploadTokenToQiniu dto);
    }
}
