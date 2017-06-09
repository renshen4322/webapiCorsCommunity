using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Common
{
    /// <summary>
    /// 获取七牛云上传token，只能上传upload空间
    /// </summary>
   public class GetUploadTokenToQiniu:CommandRequestDto
    {
     //  /// <summary>
     //  /// 文件名
     //  /// </summary>
     //  //[Required]
     ////  public string file_name { get; set; }
    }

    /// <summary>
    /// 返回七牛云指定空间的上传凭证
    /// </summary>
    public class GetUploadTokenToQiniuResponse : CommandResponseDto
    {
        /// <summary>
        /// 上传token
        /// </summary>
        public string token { get; set; }
    }
}
