using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class AsiMap : EntityTypeConfiguration<Asi>
    {
        public AsiMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Adi)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Kontrendikasyon)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Asi");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.MinUygulamaYasi).HasColumnName("MinUygulamaYasi");
            this.Property(t => t.MaxUygulamaYasi).HasColumnName("MaxUygulamaYasi");
            this.Property(t => t.IslemPeriyodu).HasColumnName("IslemPeriyodu");
            this.Property(t => t.Kontrendikasyon).HasColumnName("Kontrendikasyon");
            this.Property(t => t.EklenmeTarihi).HasColumnName("EklenmeTarihi");
            this.Property(t => t.DuzenlenmeTarihi).HasColumnName("DuzenlenmeTarihi");
        }
    }
}
