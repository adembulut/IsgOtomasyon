using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class OlayYeriFotoMap : EntityTypeConfiguration<OlayYeriFoto>
    {
        public OlayYeriFotoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ResimYolu)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("OlayYeriFoto");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ItemID).HasColumnName("ItemID");
            this.Property(t => t.ResimYolu).HasColumnName("ResimYolu");
            this.Property(t => t.EklenmeTarihi).HasColumnName("EklenmeTarihi");

            // Relationships
            this.HasOptional(t => t.Item)
                .WithMany(t => t.OlayYeriFotoes)
                .HasForeignKey(d => d.ItemID);

        }
    }
}
