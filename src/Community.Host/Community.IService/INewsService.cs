using Community.Contact.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IService
{
    public interface INewsService
    {
        Task<GetNewsListResponse> GetNewsListAsync(GetNewsList dto);
        Task<CreateNewsResponse> CreateNewsAsync(CreateNews dto);
        Task<UpdateNewsOffLineStatusResponse> UpdateNewsOffLineStatusAsync(UpdateNewsOffLineStatus dto);
        Task<UpdateNewsHotStatusResponse> UpdateNewsHotStatusAsync(UpdateNewsHotStatus dto);
        Task<UpdateNewsResponse> UpdateNewsAsync(UpdateNews dto);

    }
}
