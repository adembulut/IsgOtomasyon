using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class CalisanAsiMap : EntityTypeConfiguration<CalisanAsi>
    {
        public CalisanAsiMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Aciklama)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("CalisanAsi");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AsiID).HasColumnName("AsiID");
            this.Property(t => t.CalisanID).HasColumnName("CalisanID");
            this.Property(t => t.AsiTarihi).HasColumnName("AsiTarihi");
            this.Property(t => t.DoktorID).HasColumnName("DoktorID");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");

            // Relationships
            this.HasRequired(t => t.Asi)
                .WithMany(t => t.CalisanAsis)
                .HasForeignKey(d => d.AsiID);
            this.HasRequired(t => t.Calisan)
                .WithMany(t => t.CalisanAsis)
                .HasForeignKey(d => d.CalisanID);
            this.HasRequired(t => t.Doktor)
                .WithMany(t => t.CalisanAsis)
                .HasForeignKey(d => d.DoktorID);

        }
    }
}
