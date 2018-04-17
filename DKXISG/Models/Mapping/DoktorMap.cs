using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class DoktorMap : EntityTypeConfiguration<Doktor>
    {
        public DoktorMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.KullaniciAdi)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Parola)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.AdiSoyadi)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Telefon)
                .HasMaxLength(50);

            this.Property(t => t.Mail)
                .HasMaxLength(50);

            this.Property(t => t.Adres)
                .HasMaxLength(200);

            this.Property(t => t.DiplomaNo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Doktor");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.KullaniciAdi).HasColumnName("KullaniciAdi");
            this.Property(t => t.Parola).HasColumnName("Parola");
            this.Property(t => t.AdiSoyadi).HasColumnName("AdiSoyadi");
            this.Property(t => t.Telefon).HasColumnName("Telefon");
            this.Property(t => t.Mail).HasColumnName("Mail");
            this.Property(t => t.Adres).HasColumnName("Adres");
            this.Property(t => t.DiplomaNo).HasColumnName("DiplomaNo");
            this.Property(t => t.EkleyenID).HasColumnName("EkleyenID");
            this.Property(t => t.EklenmeTarihi).HasColumnName("EklenmeTarihi");
            this.Property(t => t.AktifMi).HasColumnName("AktifMi");

            // Relationships
            this.HasOptional(t => t.Yonetici)
                .WithMany(t => t.Doktors)
                .HasForeignKey(d => d.EkleyenID);

        }
    }
}
