using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communiry.Entity.Comment;

namespace Community.EntityFramework.Mapping.Comment
{
    public class CommentDeleteHistoryMap : CmEntityTypeConfiguration<CommentDeleteHistoryEntity>
    {
        public CommentDeleteHistoryMap()
        {
            this.ToTable("community_comment_delete_history");
            this.HasKey(t => t.Id);
            this.Property(t => t.CommentId).HasColumnName("comment_id");
            this.Property(t => t.Reason).HasColumnName("reason");
            this.Property(t => t.GMTCreate).HasColumnName("gmt_create");
            this.Property(t => t.GMTModified).HasColumnName("gmt_modified");

        }
    }
}
