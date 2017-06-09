using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
   public class GetClassifyListRequestDto:BaseRequestDto
    {
    }
   public class GetClassifyListResponseDto : BaseResponseDto
   {
       public GetClassifyListResponseDto()
       {
           data=new List<Classify>();
       }

       public List<Classify> data { get; set; }
   }

    public class Classify
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int order { get; set; }
        public long created_at { get; set; }
    }
}
