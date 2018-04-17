using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class YoneticiMap : EntityTypeConfiguration<Yonetici>
    {
        public YoneticiMap()
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
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Yonetici");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.KullaniciAdi).HasColumnName("KullaniciAdi");
            this.Property(t => t.Parola).HasColumnName("Parola");
            this.Property(t => t.AdiSoyadi).HasColumnName("AdiSoyadi");
            this.Property(t => t.Telefon).HasColumnName("Telefon");
            this.Property(t => t.Mail).HasColumnName("Mail");
            this.Property(t => t.Adres).HasColumnName("Adres");
            this.Property(t => t.AktifMi).HasColumnName("AktifMi");
        }
    }
}
