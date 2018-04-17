using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class IlceMap : EntityTypeConfiguration<Ilce>
    {
        public IlceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.IlceAdi)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Ilce");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IlID).HasColumnName("IlID");
            this.Property(t => t.IlceAdi).HasColumnName("IlceAdi");

            // Relationships
            this.HasOptional(t => t.Il)
                .WithMany(t => t.Ilces)
                .HasForeignKey(d => d.IlID);

        }
    }
}
