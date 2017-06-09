using Community.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communiry.Entity
{
   public class CityEntity:BaseEntity
    {
       public int Id { get; set; }
       public int ProvinceId { get; set; }
       public string Name { get; set; }
       public string AreaCode { get; set; }
    }
}
