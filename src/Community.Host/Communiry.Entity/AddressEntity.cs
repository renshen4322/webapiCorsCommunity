using Communiry.Entity.EnumType;
using Community.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communiry.Entity
{
   public class AddressEntity:BaseEntity
    {
       public int Id { get; set; }
       public Guid UserId { get; set; }
     
       public AddressTypeEnum Type {
           get {
               return (AddressTypeEnum)Enum.Parse(typeof(AddressTypeEnum), DbType, true);
           }
           set {
               DbType = value.ToString();
           }
       
       }
       public string DbType { get; set; }
       public int? CountryId { get; set; }
       public int? ProvinceId { get; set; }
       public int? CityId { get; set; }
       public int? DistrictId { get; set; }
       public DateTime CreatedDate { get; set; }

    }
}
