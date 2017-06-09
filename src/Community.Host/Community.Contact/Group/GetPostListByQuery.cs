using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
   public class GetPostListByQueryRequestDto:QueryRequestDto
    {
       /// <summary>
       /// 标题
       /// </summary>
        [Required]
       public string q { get; set; }

    }

    public class GetPostListByQueryResponseDto : QueryResponseDto
    {
        public GetPostListByQueryResponseDto()
        {
            data = new List<PostInfo>();
        }
        public List<PostInfo> data { get; set; }
    }


   
}
