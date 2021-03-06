﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
   public class CreateClassifyRequestDto:CommandRequestDto
    {
       /// <summary>
       /// 板块名
       /// </summary>
       [Required]
       public string name { get; set; }

       /// <summary>
       /// 板块描述
       /// </summary>
       [Required]
       public string description { get; set; }

       /// <summary>
       /// 排序
       /// </summary>
       public int order { get; set; }
    }
   public class CreateClassifyResponseDto : CommandResponseDto
   {
       public int id { get; set; }
   }
}
