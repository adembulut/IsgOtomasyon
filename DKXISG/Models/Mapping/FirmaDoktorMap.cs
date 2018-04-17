using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class FirmaDoktorMap : EntityTypeConfiguration<FirmaDoktor>
    {
        public FirmaDoktorMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("FirmaDoktor");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FirmaID).HasColumnName("FirmaID");
            this.Property(t => t.DoktorID).HasColumnName("DoktorID");
            this.Property(t => t.AtanmaTarihi).HasColumnName("AtanmaTarihi");
            this.Property(t => t.AtayanID).HasColumnName("AtayanID");

            // Relationships
            this.HasRequired(t => t.Doktor)
                .WithMany(t => t.FirmaDoktors)
                .HasForeignKey(d => d.DoktorID);
            this.HasRequired(t => t.Firma)
                .WithMany(t => t.FirmaDoktors)
                .HasForeignKey(d => d.FirmaID);
            this.HasRequired(t => t.Yonetici)
                .WithMany(t => t.FirmaDoktors)
                .HasForeignKey(d => d.AtayanID);

        }
    }
}
