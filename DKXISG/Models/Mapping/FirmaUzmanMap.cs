using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class FirmaUzmanMap : EntityTypeConfiguration<FirmaUzman>
    {
        public FirmaUzmanMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("FirmaUzman");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FirmaID).HasColumnName("FirmaID");
            this.Property(t => t.UzmanID).HasColumnName("UzmanID");
            this.Property(t => t.AtanmaTarihi).HasColumnName("AtanmaTarihi");
            this.Property(t => t.AtayanID).HasColumnName("AtayanID");

            // Relationships
            this.HasRequired(t => t.Firma)
                .WithMany(t => t.FirmaUzmen)
                .HasForeignKey(d => d.FirmaID);
            this.HasRequired(t => t.Uzman)
                .WithMany(t => t.FirmaUzmen)
                .HasForeignKey(d => d.UzmanID);
            this.HasRequired(t => t.Yonetici)
                .WithMany(t => t.FirmaUzmen)
                .HasForeignKey(d => d.AtayanID);

        }
    }
}
