using Community.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communiry.Entity
{
    public class TagEntity : BaseEntity
    {
       public int Id { get; set; }
       public int TypeId { get; set; }
       public bool IsSystemTag{get;set;}
       public string name { get; set; }
       public bool OffLine { get; set; }
       public bool IsHot { get; set; }
       public int DisplayIndex { get; set; }
       public DateTime CreatedDate { get; set; }
    }
}
