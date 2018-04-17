using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class SahaDenetimMap : EntityTypeConfiguration<SahaDenetim>
    {
        public SahaDenetimMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Adi)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SahaDenetim");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.UzmanID).HasColumnName("UzmanID");
            this.Property(t => t.FirmaID).HasColumnName("FirmaID");
            this.Property(t => t.EklenenTarih).HasColumnName("EklenenTarih");
            this.Property(t => t.RaporTarihi).HasColumnName("RaporTarihi");

            // Relationships
            this.HasRequired(t => t.Firma)
                .WithMany(t => t.SahaDenetims)
                .HasForeignKey(d => d.FirmaID);
            this.HasRequired(t => t.Uzman)
                .WithMany(t => t.SahaDenetims)
                .HasForeignKey(d => d.UzmanID);

        }
    }
}
