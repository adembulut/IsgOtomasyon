using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class CalismaMap : EntityTypeConfiguration<Calisma>
    {
        public CalismaMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CalismayiYapanKisi)
                .HasMaxLength(50);

            this.Property(t => t.CalismayiYapanAdiSoyadi)
                .HasMaxLength(50);

            this.Property(t => t.Aciklama)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Calisma");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CalismaPlaniID).HasColumnName("CalismaPlaniID");
            this.Property(t => t.CalismayiYapanKisi).HasColumnName("CalismayiYapanKisi");
            this.Property(t => t.CalismayiYapanAdiSoyadi).HasColumnName("CalismayiYapanAdiSoyadi");
            this.Property(t => t.Tarih).HasColumnName("Tarih");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");

            // Relationships
            this.HasOptional(t => t.CalismaPlani)
                .WithMany(t => t.Calismas)
                .HasForeignKey(d => d.CalismaPlaniID);

        }
    }
}
