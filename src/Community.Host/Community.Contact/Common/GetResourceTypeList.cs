using Community.Contact.Common.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Common
{
  public  class GetResourceTypeList:BaseRequestDto
    {

    }
  public class GetResourceTypeListResponse : BaseResponseDto
  {
      public GetResourceTypeListResponse() {
          this.data = new List<Resource>();
      }
      public List<Resource> data { get; set; }
  }
}
