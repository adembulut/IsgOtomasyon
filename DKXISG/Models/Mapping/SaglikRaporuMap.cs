using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class SaglikRaporuMap : EntityTypeConfiguration<SaglikRaporu>
    {
        public SaglikRaporuMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.HastaAdiSoyadi)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.HastaTCNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.HastaTelefon)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.HastaAdres)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("SaglikRaporu");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ZiyaretID).HasColumnName("ZiyaretID");
            this.Property(t => t.HastaAdiSoyadi).HasColumnName("HastaAdiSoyadi");
            this.Property(t => t.HastaTCNo).HasColumnName("HastaTCNo");
            this.Property(t => t.HastaTelefon).HasColumnName("HastaTelefon");
            this.Property(t => t.HastaAdres).HasColumnName("HastaAdres");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.Tarih).HasColumnName("Tarih");

            // Relationships
            this.HasRequired(t => t.DoktorZiyaret)
                .WithMany(t => t.SaglikRaporus)
                .HasForeignKey(d => d.ZiyaretID);

        }
    }
}
