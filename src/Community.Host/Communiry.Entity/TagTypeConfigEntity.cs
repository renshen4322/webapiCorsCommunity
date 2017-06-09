using Community.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communiry.Entity
{
    public class TagTypeConfigEntity : BaseEntity
    {
       public int Id { get; set; }
       public string TypeName { get; set; }
    }
}
