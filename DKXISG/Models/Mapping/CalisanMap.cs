using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class CalisanMap : EntityTypeConfiguration<Calisan>
    {
        public CalisanMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.AdiSoyadi)
                .HasMaxLength(50);

            this.Property(t => t.Gorevi)
                .HasMaxLength(50);

            this.Property(t => t.TcKimlikNo)
                .HasMaxLength(11);

            this.Property(t => t.Cinsiyet)
                .HasMaxLength(50);

            this.Property(t => t.MedeniHali)
                .HasMaxLength(50);

            this.Property(t => t.Telefon)
                .IsRequired()
                .HasMaxLength(11);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.EgitimDurumu)
                .HasMaxLength(50);

            this.Property(t => t.KanGrubu)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Calisan");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AdiSoyadi).HasColumnName("AdiSoyadi");
            this.Property(t => t.Gorevi).HasColumnName("Gorevi");
            this.Property(t => t.DogumTarihi).HasColumnName("DogumTarihi");
            this.Property(t => t.TcKimlikNo).HasColumnName("TcKimlikNo");
            this.Property(t => t.Cinsiyet).HasColumnName("Cinsiyet");
            this.Property(t => t.MedeniHali).HasColumnName("MedeniHali");
            this.Property(t => t.Telefon).HasColumnName("Telefon");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.EgitimDurumu).HasColumnName("EgitimDurumu");
            this.Property(t => t.KanGrubu).HasColumnName("KanGrubu");
            this.Property(t => t.IsyeriBolumID).HasColumnName("IsyeriBolumID");
            this.Property(t => t.IseGirisTarihi).HasColumnName("IseGirisTarihi");
            this.Property(t => t.EklenmeTarihi).HasColumnName("EklenmeTarihi");
            this.Property(t => t.HalaCalisiyorMu).HasColumnName("HalaCalisiyorMu");

            // Relationships
            this.HasOptional(t => t.IsyeriBolumu)
                .WithMany(t => t.Calisans)
                .HasForeignKey(d => d.IsyeriBolumID);

        }
    }
}
