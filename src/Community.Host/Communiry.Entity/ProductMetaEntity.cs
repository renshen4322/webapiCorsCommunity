using Community.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communiry.Entity
{
    public class ProductMetaEntity : BaseEntity
    {
       public int Id { get; set; }
       public Guid PruductId { get; set; }
       public bool IsHot { get; set; }
       
   }
}
