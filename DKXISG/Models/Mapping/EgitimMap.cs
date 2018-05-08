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
            this.Property(t => t.Adi)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Lokasyonu)
                .HasMaxLength(50);

            this.Property(t => t.EgitimYapan)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EgitimEkleyen)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EkleyenAdiSoyadi)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Egitim");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.EgitimTarihi).HasColumnName("EgitimTarihi");
            this.Property(t => t.Lokasyonu).HasColumnName("Lokasyonu");
            this.Property(t => t.isYapildi).HasColumnName("isYapildi");
            this.Property(t => t.EgitimYapan).HasColumnName("EgitimYapan");
            this.Property(t => t.EgitimEkleyen).HasColumnName("EgitimEkleyen");
            this.Property(t => t.EkleyenAdiSoyadi).HasColumnName("EkleyenAdiSoyadi");
            this.Property(t => t.FirmaID).HasColumnName("FirmaID");
            this.Property(t => t.EklenmeTarihi).HasColumnName("EklenmeTarihi");

            // Relationships
            this.HasRequired(t => t.Firma)
                .WithMany(t => t.Egitims)
                .HasForeignKey(d => d.FirmaID);

        }
    }
}
