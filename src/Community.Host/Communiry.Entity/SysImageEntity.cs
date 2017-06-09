using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Core.Data;

namespace Communiry.Entity
{
   public class SysImageEntity:BaseEntity
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
