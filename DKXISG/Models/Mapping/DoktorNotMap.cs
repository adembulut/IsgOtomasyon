using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class DoktorNotMap : EntityTypeConfiguration<DoktorNot>
    {
        public DoktorNotMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Baslik)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Aciklama)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("DoktorNot");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DoktorZiyaretID).HasColumnName("DoktorZiyaretID");
            this.Property(t => t.Baslik).HasColumnName("Baslik");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.EklenmeTarihi).HasColumnName("EklenmeTarihi");

            // Relationships
            this.HasOptional(t => t.DoktorZiyaret)
                .WithMany(t => t.DoktorNots)
                .HasForeignKey(d => d.DoktorZiyaretID);

        }
    }
}
