using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class EgitimMap : EntityTypeConfiguration<Egitim>
    {
        public EgitimMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Konusu)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Egitim");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Konusu).HasColumnName("Konusu");
            this.Property(t => t.Suresi).HasColumnName("Suresi");
        }
    }
}
