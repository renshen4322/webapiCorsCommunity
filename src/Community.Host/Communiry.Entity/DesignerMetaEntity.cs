using Community.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communiry.Entity
{
    public class DesignerMetaEntity : BaseEntity
    {
       public Guid Id { get; set; }
       public Guid BaseUserId { get; set; }
       public int DesignAge { get; set; }
       public DateTime CreatedDate { get; set; }
    }
}
