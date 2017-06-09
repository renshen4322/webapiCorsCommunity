using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Core.Data;

namespace Communiry.Entity.Comment
{
   public class CommentDeleteHistoryEntity:BaseEntity
    {
       public int Id { get; set; }
       public Guid CommentId { get; set; }
       public string Reason { get; set; }
       public DateTime GMTCreate { get; set; }
       public DateTime? GMTModified { get; set; }
    }
}
