using Community.Contact.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IService
{
    public interface IProductService
    {
        
        Task<GetProductDetailByIdResponse> GetDetailAsync(GetProductDetailById dto);
      
        Task<GetProductListResponse> GetListAsync(GetProductList dto);
     
        Task<UpdateProductHotStatusResponse> UpdateHotStatusAsync(UpdateProductHotStatus dto);

       
        Task<UpdateProductOffLineStatusResponse> UpdateOffLineStatusAsync(UpdateProductOffLineStatus dto);
       
    }
}
