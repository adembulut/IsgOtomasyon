using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class FirmaYapilacakMap : EntityTypeConfiguration<FirmaYapilacak>
    {
        public FirmaYapilacakMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Tipi)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Aciklama)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("FirmaYapilacak");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Tipi).HasColumnName("Tipi");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.isTamam).HasColumnName("isTamam");
            this.Property(t => t.Tarih).HasColumnName("Tarih");
            this.Property(t => t.FirmaID).HasColumnName("FirmaID");
            this.Property(t => t.UzmanID).HasColumnName("UzmanID");
            this.Property(t => t.okTarih).HasColumnName("okTarih");

            // Relationships
            this.HasRequired(t => t.Firma)
                .WithMany(t => t.FirmaYapilacaks)
                .HasForeignKey(d => d.FirmaID);
            this.HasRequired(t => t.Uzman)
                .WithMany(t => t.FirmaYapilacaks)
                .HasForeignKey(d => d.UzmanID);

        }
    }
}
