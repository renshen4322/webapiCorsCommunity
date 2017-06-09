using Community.Contact.Works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IService
{
    public interface IWorksService
    {
        Task<GetWorksDetailResponse> GetDetailAsync(GetWorksDetail dto);

        Task<GetWorksListResponse> GetListAsync(GetWorksList dto);

        Task<UpdateWorksHotStatusResponse> UpdateHotStatusAsync(UpdateWorksHotStatus dto);


        Task<UpdateWorksOffLineStatusResponse> UpdateOffLineStatusAsync(UpdateWorksOffLineStatus dto);
    }
}
