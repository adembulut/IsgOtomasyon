using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DKXISG.Models.Mapping
{
    public class ItemMap : EntityTypeConfiguration<Item>
    {
        public ItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TehlikeSinifi)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TEDurum)
                .IsRequired();

            this.Property(t => t.Riskler)
                .IsRequired();

            this.Property(t => t.DOFaliyetler)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Item");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TehlikeSinifi).HasColumnName("TehlikeSinifi");
            this.Property(t => t.TEDurum).HasColumnName("TEDurum");
            this.Property(t => t.Riskler).HasColumnName("Riskler");
            this.Property(t => t.Olasilik).HasColumnName("Olasilik");
            this.Property(t => t.Siddet).HasColumnName("Siddet");
            this.Property(t => t.Risk).HasColumnName("Risk");
            this.Property(t => t.OncelikSirasi).HasColumnName("OncelikSirasi");
            this.Property(t => t.DOFaliyetler).HasColumnName("DOFaliyetler");
            this.Property(t => t.TerminTarihi).HasColumnName("TerminTarihi");
            this.Property(t => t.EklenmeTarihi).HasColumnName("EklenmeTarihi");
            this.Property(t => t.SDID).HasColumnName("SDID");

            // Relationships
            this.HasOptional(t => t.SahaDenetim)
                .WithMany(t => t.Items)
                .HasForeignKey(d => d.SDID);

        }
    }
}
