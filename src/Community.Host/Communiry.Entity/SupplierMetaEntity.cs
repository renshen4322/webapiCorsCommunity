using Community.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communiry.Entity
{
    public class SupplierMetaEntity : BaseEntity
    {
       public Guid Id { get; set; }
       public Guid BaseUserId { get; set; }
       public string Moblie { get; set; }
       public DateTime CreatedDate { get; set; }
    }
}
