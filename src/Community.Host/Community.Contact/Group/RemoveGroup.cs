using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
   public class RemoveGroupRequestDto:CommandRequestDto
    {
       /// <summary>
       /// 组id
       /// </summary>
       public int group_id { get; set; }
       /// <summary>
       /// 上下线状态
       /// </summary>
       public bool offline { get; set; }
    }

    public class RemoveGroupResponseDto : CommandResponseDto
    {

    }
}
