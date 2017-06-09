using Community.Contact.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IService
{
    public interface ICategoryService
    {
        Task<GetTargetAllCategoryResponse> GetAllCategoryAsync(GetTargetAllCategory dto);
        Task<CreateCategoryResponse> CreateCategoryAsync(CreateCategory dto);
        Task<UpdateCategoryDisplayIndexResponse> UpdateDisplayIndexAsync(UpdateCategoryDisplayIndex dto);
        Task<UpdateCategoryHotStatusResponse> UpdateHotStatusAsync(UpdateCategoryHotStatus dto);
        Task<UpdateCategoryOffLineStatusResponse> UpdateOffLineStatusAsync(UpdateCategoryOffLineStatus dto);
        Task<UpdateObjectCategoryResponse> UpdateCategoryAsync(UpdateObjectCategory dto);
    }
}
