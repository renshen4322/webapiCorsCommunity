using AutoMapper;
using Communiry.Entity;
using Community.Common;
using Community.Contact.Common;
using Community.Contact.Common.Component;
using Community.Core.Data;
using Community.IService;
using Community.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;


namespace Community.Service
{
    
   public class CommonService:ICommonService
   {
       #region Fields
       private readonly IRepository<ProvinceEntity> _provinceRepository;
       private readonly IRepository<CityEntity> _cityRepository;
       private readonly IRepository<DistrictEntity> _districtRepository;
       private readonly IRepository<CategoryTypeConfigEntity> _categoryTypeConfigRepository;

       const string CACHE_RESOURCE_LIST = "ResourceTypeList";
       #endregion

       #region Ctor
       public CommonService(IRepository<ProvinceEntity> provinceRepository,
           IRepository<CityEntity> cityRepository,
           IRepository<DistrictEntity> districtRepository,
           IRepository<CategoryTypeConfigEntity> categoryTypeConfigRepository
           ) {
           this._provinceRepository = provinceRepository;
           this._cityRepository = cityRepository;
           this._districtRepository = districtRepository;
           this._categoryTypeConfigRepository = categoryTypeConfigRepository;
       }
       #endregion
       public Task<List<GetProvinceResponse>> GetProvinceAsync(GetProvince dto)
        {
            
              return Task.Run(() =>
            {
                List<ProvinceEntity> list = CacheHelper.Get<List<ProvinceEntity>>(ServiceGlobalConfig.CACHE_PROVINCE_LIST);
                if (list == null)
                {
                     list = _provinceRepository.Table.ToList();
                     CacheHelper.Insert<List<ProvinceEntity>>(ServiceGlobalConfig.CACHE_PROVINCE_LIST, list, 60);
                  
                }
                return Mapper.Map<List<GetProvinceResponse>>(list); ;
               
            });
         
        }


        public Task<List<GetCityResponse>> GetCityAsync(GetCity dto)
        {
            return Task.Run(() =>
            {
                string cacheName = string.Format(ServiceGlobalConfig.CACHE_CITY_LIST, dto.province_id);
                List<CityEntity> list = CacheHelper.Get<List<CityEntity>>(cacheName);
             
                if (list==null)
                {
                    list = _cityRepository.Table.Where(t => t.ProvinceId == dto.province_id).ToList();
                    CacheHelper.Insert<List<CityEntity>>(cacheName, list, 60);
                }

                return Mapper.Map<List<GetCityResponse>>(list); ;
            });
        }

        public Task<List<GetDistrictResponse>> GetDistrictAsync(GetDistrict dto)
        {
            return Task.Run(() =>
            {
                string cacheName = string.Format(ServiceGlobalConfig.CACHE_DISTRICT_LIST, dto.city_id);
                List<GetDistrictResponse> list = CacheHelper.Get<List<GetDistrictResponse>>(cacheName);
                if (list==null)
                {
                    var cityList = _districtRepository.Table.Where(t => t.CityId == dto.city_id).ToList();
                    list= Mapper.Map<List<GetDistrictResponse>>(cityList);
                    CacheHelper.Insert<List<GetDistrictResponse>>(cacheName, list, 60);
                }
                return list;
               
            });
        }
        public Task<GetResourceTypeListResponse> GetResourceTypeListAsync(GetResourceTypeList dto)
        {
            return Task.Run(() =>
            {
                GetResourceTypeListResponse resp = new GetResourceTypeListResponse();

                List<Resource> data = CacheHelper.Get<List<Resource>>(CACHE_RESOURCE_LIST);
              if (data==null)
              {
                  var dbData = _categoryTypeConfigRepository.TableNoTracking.ToList();
                  data = Mapper.Map<List<Resource>>(dbData);
                  CacheHelper.Insert<List<Resource>>(CACHE_RESOURCE_LIST,data,30);
              }

              resp.data = data;
              return resp;

            });
        }
        public Task<GetUploadTokenToQiniuResponse> GetUploadTokenToQiniuAsync(GetUploadTokenToQiniu dto)
        {
            return Task.Run(() =>
            {
             
                // PutPolicy            
                PutPolicy putPolicy = new PutPolicy();
                putPolicy.Scope = GlobalAppSettings.QiniuScope;
                // 上传策略有效时间(建议时间3600秒)
                putPolicy.SetExpires(GlobalAppSettings.QiniuExpires);
                
                // UploadToken
                Mac mac = new Mac(GlobalAppSettings.QiniuAccessKey, GlobalAppSettings.QiniuSecretKey);
                string token = Auth.createUploadToken(putPolicy, mac);
                var resp = new GetUploadTokenToQiniuResponse();
                resp.token = token;
                return resp;
            });
        }





       
   }
}
