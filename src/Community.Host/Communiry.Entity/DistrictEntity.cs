using Community.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communiry.Entity
{
  public class DistrictEntity:BaseEntity
    {
      public int Id { get; set; }
      public int CityId { get; set; }
      public string Name { get; set; }
      public string PostCode { get; set; }
    }
}
