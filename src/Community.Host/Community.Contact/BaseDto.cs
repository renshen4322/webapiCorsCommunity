using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact
{
    public abstract class BaseRequestDto
    {

        /// <summary>
        /// the device name of the client
        /// </summary>
        // public string Device { get; set; }

        /// <summary>
        /// the device OS of the client
        /// </summary>
        //public string DeviceOS { get; set; }

        /// <summary>
        /// the device version of the client
        /// </summary>
        //public string DeviceVersion { get; set; }

    }

    public abstract class BaseResponseDto
    {

    }

    public  class CommandRequestDto : BaseRequestDto
    {
      //  public string UserId { get; set; }

        public CommandRequestDto() : base() { }
    }

    public abstract class CommandResponseDto : BaseResponseDto
    {
        public CommandResponseDto() : base() { }
    }
    public abstract class QueryRequestDto : BaseRequestDto
    {

        /// <summary>
        /// indicate sorting order, e.g. ?sort=-Name, -Name means order by Name descendingly
        /// </summary>
       // public string Sort { get; set; }

        /// <summary>
        /// 起始条数 ?start=50&length=100
        /// </summary>
        [Required]
        public int start { get; set; }

        /// <summary>
        ///获取记录数量 ?start=50&length=100
        /// </summary>
        [Required]
        public int length { get; set; }

        public QueryRequestDto()
            : base()
        {
            start =0;
            length =20;
        }
    }

    public abstract class QueryResponseDto : BaseResponseDto
    {
        public QueryResponseDto() : base() {
            total = 0;
        }
        public long? total { get; set; }
      //  public MetaData _metaData { get; set; }
    }
    public class MetaData
    {
        public long? Total { get; set; }
        public int? Limit { get; set; }
        public long? Offset { get; set; }
    }
}
