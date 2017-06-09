using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communiry.Entity;

namespace Community.EntityFramework.Mapping
{
    public class SysImageMap : CmEntityTypeConfiguration<SysImageEntity>
    {
        public SysImageMap()
        {
            this.ToTable("community_sys_image");
            this.HasKey(t => t.Id);
            this.Property(t => t.ProjectId).HasColumnName("project_id");
            this.Property(t => t.ImageName).HasColumnName("image_name");
            this.Property(t => t.Description).HasColumnName("description");
            this.Property(t => t.ImageUri).HasColumnName("image_uri");
            this.Property(t => t.CreatedDate).HasColumnName("created_date");
        }
    }
}
