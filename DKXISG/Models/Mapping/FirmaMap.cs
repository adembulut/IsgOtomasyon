using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class FirmaMap : EntityTypeConfiguration<Firma>
    {
        public FirmaMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Adi)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Adresi)
                .HasMaxLength(250);

            this.Property(t => t.Telefonu)
                .HasMaxLength(50);

            this.Property(t => t.Sinifi)
                .HasMaxLength(50);

            this.Property(t => t.VergiDairesi)
                .HasMaxLength(50);

            this.Property(t => t.VergiNumarasi)
                .HasMaxLength(50);

            this.Property(t => t.LatLng)
                .HasMaxLength(50);

            this.Property(t => t.Web)
                .HasMaxLength(50);

            this.Property(t => t.Mail)
                .HasMaxLength(50);

            this.Property(t => t.ResimYolu)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Firma");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.IlceID).HasColumnName("IlceID");
            this.Property(t => t.Adresi).HasColumnName("Adresi");
            this.Property(t => t.Telefonu).HasColumnName("Telefonu");
            this.Property(t => t.Sinifi).HasColumnName("Sinifi");
            this.Property(t => t.VergiDairesi).HasColumnName("VergiDairesi");
            this.Property(t => t.VergiNumarasi).HasColumnName("VergiNumarasi");
            this.Property(t => t.LatLng).HasColumnName("LatLng");
            this.Property(t => t.Web).HasColumnName("Web");
            this.Property(t => t.Mail).HasColumnName("Mail");
            this.Property(t => t.CalisanSayisi).HasColumnName("CalisanSayisi");
            this.Property(t => t.MusteriID).HasColumnName("MusteriID");
            this.Property(t => t.EkleyenID).HasColumnName("EkleyenID");
            this.Property(t => t.EklenmeTarihi).HasColumnName("EklenmeTarihi");
            this.Property(t => t.TamamlandiMi).HasColumnName("TamamlandiMi");
            this.Property(t => t.SektorId).HasColumnName("SektorId");
            this.Property(t => t.ResimYolu).HasColumnName("ResimYolu");

            // Relationships
            this.HasOptional(t => t.Ilce)
                .WithMany(t => t.Firmas)
                .HasForeignKey(d => d.IlceID);
            this.HasRequired(t => t.Musteri)
                .WithMany(t => t.Firmas)
                .HasForeignKey(d => d.MusteriID);
            this.HasRequired(t => t.Sektor)
                .WithMany(t => t.Firmas)
                .HasForeignKey(d => d.SektorId);
            this.HasRequired(t => t.Yonetici)
                .WithMany(t => t.Firmas)
                .HasForeignKey(d => d.EkleyenID);

        }
    }
}
