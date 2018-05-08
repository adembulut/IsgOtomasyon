using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class CalismaPlaniMap : EntityTypeConfiguration<CalismaPlani>
    {
        public CalismaPlaniMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Faliyet)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.PeriyotTipi)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.EkleyenKisi)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EkleyenAdi)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SorumluKisi)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Aciklama)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("CalismaPlani");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Faliyet).HasColumnName("Faliyet");
            this.Property(t => t.FirmaID).HasColumnName("FirmaID");
            this.Property(t => t.PeriyotTipi).HasColumnName("PeriyotTipi");
            this.Property(t => t.PeriyotAraligi).HasColumnName("PeriyotAraligi");
            this.Property(t => t.EkleyenKisi).HasColumnName("EkleyenKisi");
            this.Property(t => t.EkleyenAdi).HasColumnName("EkleyenAdi");
            this.Property(t => t.SorumluKisi).HasColumnName("SorumluKisi");
            this.Property(t => t.EklenmeTarihi).HasColumnName("EklenmeTarihi");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");

            // Relationships
            this.HasRequired(t => t.Firma)
                .WithMany(t => t.CalismaPlanis)
                .HasForeignKey(d => d.FirmaID);

        }
    }
}
