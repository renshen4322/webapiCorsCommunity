using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Core.Data;

namespace Communiry.Entity
{
  public class SysProjectNameEntity:BaseEntity
    {
      public int Id { get; set; }
      public string Name { get; set; }
      public DateTime CreatedDate { get; set; }
    }
}
