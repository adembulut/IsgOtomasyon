using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class DoktorZiyaretMap : EntityTypeConfiguration<DoktorZiyaret>
    {
        public DoktorZiyaretMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("DoktorZiyaret");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DoktorID).HasColumnName("DoktorID");
            this.Property(t => t.FirmaID).HasColumnName("FirmaID");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.Tarih).HasColumnName("Tarih");

            // Relationships
            this.HasOptional(t => t.Doktor)
                .WithMany(t => t.DoktorZiyarets)
                .HasForeignKey(d => d.DoktorID);
            this.HasOptional(t => t.Firma)
                .WithMany(t => t.DoktorZiyarets)
                .HasForeignKey(d => d.FirmaID);

        }
    }
}
