using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class SaglikRaporuTetkikMap : EntityTypeConfiguration<SaglikRaporuTetkik>
    {
        public SaglikRaporuTetkikMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Tetkik)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SaglikRaporuTetkik");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SaglikRaporuID).HasColumnName("SaglikRaporuID");
            this.Property(t => t.Tetkik).HasColumnName("Tetkik");
            this.Property(t => t.Sonuc).HasColumnName("Sonuc");
            this.Property(t => t.Tarih).HasColumnName("Tarih");

            // Relationships
            this.HasRequired(t => t.SaglikRaporu)
                .WithMany(t => t.SaglikRaporuTetkiks)
                .HasForeignKey(d => d.SaglikRaporuID);

        }
    }
}
