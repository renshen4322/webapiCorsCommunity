using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
    /// <summary>
    /// 删除板块
    /// </summary>
   public class RemoveClassifyRequestDto:CommandRequestDto
    {
       /// <summary>
       /// 板块id
       /// </summary>
       public int classify_id { get; set; }
    }
   public class RemoveClassifyResponseDto : CommandResponseDto
   {

   }
}
