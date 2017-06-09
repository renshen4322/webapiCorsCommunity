using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communiry.Entity;

namespace Community.EntityFramework.Mapping
{
    public class SysProjectNameMap : CmEntityTypeConfiguration<SysProjectNameEntity>
    {
        public SysProjectNameMap()
        {
            this.ToTable("community_sys_project_name");
            this.HasKey(t => t.Id);
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.CreatedDate).HasColumnName("created_date");
        }
    }
}
