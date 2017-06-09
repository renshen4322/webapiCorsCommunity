using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
   public class UpdateClassifyOrderRequestDto:CommandRequestDto
    {
       /// <summary>
       /// 板块id
       /// </summary>
       public int classify_id { get; set; }

       /// <summary>
       /// 顺序
       /// </summary>
       public int order { get; set; }
    }
   public class UpdateClassifyOrderResponseDto : CommandResponseDto
   {
      
   }
}
