using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class IsyeriBolumuMap : EntityTypeConfiguration<IsyeriBolumu>
    {
        public IsyeriBolumuMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Adi)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("IsyeriBolumu");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.FirmaID).HasColumnName("FirmaID");

            // Relationships
            this.HasOptional(t => t.Firma)
                .WithMany(t => t.IsyeriBolumus)
                .HasForeignKey(d => d.FirmaID);

        }
    }
}
