using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Communiry.Entity;
using Community.Contact.Image;
using Community.Contact.News.Component;
using Community.Core.Data;
using Community.IService;
using Community.Service.Const;
using Community.Service.Model;

namespace Community.Service
{
    public class ImageService : IImageService
    {
          #region Fields
        private readonly IRepository<SysImageEntity> _sysImageRepository;
        private readonly IRepository<SysProjectNameEntity> _sysProjectNameRepository;
        private readonly IDapperRepository _dapperRepository;
       #endregion

       #region Ctor
        public ImageService(IRepository<SysImageEntity> sysImageRepository,
          IRepository<SysProjectNameEntity> sysProjectNameRepository,
            IDapperRepository dapperRepository)
        {
            _sysImageRepository = sysImageRepository;
            _sysProjectNameRepository = sysProjectNameRepository;
            _dapperRepository = dapperRepository;
        }
       #endregion


        public Task<Contact.Image.GetSystemImageListResponse> GetSystemImageListAsync(Contact.Image.GetSystemImageList dto)
        {
            return Task.Run(() =>
            {
                List<SelectProjectImageModel> list;
                int total =0;
                if (dto.project_id==null)
                {
                  list=  _dapperRepository.Query<SelectProjectImageModel>(
                       string.Format(ImageServiceConst.SELECT_SYSTEM_IMG_SQL,""),new{start=dto.start,length=dto.length}).ToList();
                  total = _dapperRepository.QuerySingleOrDefault<int>(string.Format(ImageServiceConst.SELECT_SYSTEM_IMG_COUNT_SQL, ""));
                }
                else
                {
                    list = _dapperRepository.Query<SelectProjectImageModel>(
                        string.Format(ImageServiceConst.SELECT_SYSTEM_IMG_SQL,
                            ImageServiceConst.SELECT_SYSTEM_IMG_WHERE_SQL), new { projectId = dto.project_id, start = dto.start, length = dto.length }).ToList();
                    total = _dapperRepository.QuerySingleOrDefault<int>(string.Format(ImageServiceConst.SELECT_SYSTEM_IMG_COUNT_SQL, ImageServiceConst.SELECT_SYSTEM_IMG_WHERE_SQL), new { projectId = dto.project_id });
                }
                GetSystemImageListResponse resp = new GetSystemImageListResponse();
                resp.data = Mapper.Map<List<SystemImageDto>>(list);
                resp.total = total;
                return resp;


            });
        }

        public Task<Contact.Image.GetImageProjectNameResponse> GetImageProjectNameAsync(Contact.Image.GetImageProjectName dto)
        {
            return Task.Run(() =>
            {
                var list = _sysProjectNameRepository.TableNoTracking;
                GetImageProjectNameResponse resp = new GetImageProjectNameResponse();
                resp.data = Mapper.Map<List<Project>>(list);
                return resp;
            });
        }

        public Task<Contact.Image.AddSystemImageResponse> AddSystemImageAsync(Contact.Image.AddSystemImage dto)
        {
            return Task.Run(() =>
            {
               SysImageEntity sysImageEntity=new SysImageEntity()
               {
                    CreatedDate = DateTime.Now,
                    ImageName = dto.image_name,
                     ImageUri = dto.image_uri,
                     Description = dto.description,
                     ProjectId = dto.project_id

               };
                _sysImageRepository.Insert(sysImageEntity);
                return new AddSystemImageResponse() { id = sysImageEntity.Id};
            });
        }

        public Task<Contact.Image.AddProjectResponse> AddProjectAsync(Contact.Image.AddProject dto)
        {
            return Task.Run(() =>
            {
                SysProjectNameEntity sysProjectNameEntity = new SysProjectNameEntity()
                {
                    CreatedDate = DateTime.Now,
                    Name = dto.name

                };
                _sysProjectNameRepository.Insert(sysProjectNameEntity);
                return new AddProjectResponse() { id = sysProjectNameEntity.Id };
            });
        }
    }
}
