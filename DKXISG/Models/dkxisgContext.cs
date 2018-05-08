using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DKXISG.Models.Mapping;

namespace DKXISG.Models
{
    public partial class dkxisgContext : DbContext
    {
        static dkxisgContext()
        {
            Database.SetInitializer<dkxisgContext>(null);
        }

        public dkxisgContext()
            : base("Name=dkxisgContext")
        {
        }

        public DbSet<Asi> Asis { get; set; }
        public DbSet<Calisan> Calisans { get; set; }
        public DbSet<CalisanAsi> CalisanAsis { get; set; }
        public DbSet<Calisma> Calismas { get; set; }
        public DbSet<CalismaPlani> CalismaPlanis { get; set; }
        public DbSet<Doktor> Doktors { get; set; }
        public DbSet<DoktorNot> DoktorNots { get; set; }
        public DbSet<DoktorZiyaret> DoktorZiyarets { get; set; }
        public DbSet<Egitim> Egitims { get; set; }
        public DbSet<Firma> Firmas { get; set; }
        public DbSet<FirmaDoktor> FirmaDoktors { get; set; }
        public DbSet<FirmaUzman> FirmaUzmen { get; set; }
        public DbSet<FirmaYapilacak> FirmaYapilacaks { get; set; }
        public DbSet<Il> Ils { get; set; }
        public DbSet<Ilce> Ilces { get; set; }
        public DbSet<IsyeriBolumu> IsyeriBolumus { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Musteri> Musteris { get; set; }
        public DbSet<OlayYeriFoto> OlayYeriFotoes { get; set; }
        public DbSet<SaglikRaporu> SaglikRaporus { get; set; }
        public DbSet<SaglikRaporuTetkik> SaglikRaporuTetkiks { get; set; }
        public DbSet<SahaDenetim> SahaDenetims { get; set; }
        public DbSet<Sektor> Sektors { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Uzman> Uzmen { get; set; }
        public DbSet<Yonetici> Yoneticis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AsiMap());
            modelBuilder.Configurations.Add(new CalisanMap());
            modelBuilder.Configurations.Add(new CalisanAsiMap());
            modelBuilder.Configurations.Add(new CalismaMap());
            modelBuilder.Configurations.Add(new CalismaPlaniMap());
            modelBuilder.Configurations.Add(new DoktorMap());
            modelBuilder.Configurations.Add(new DoktorNotMap());
            modelBuilder.Configurations.Add(new DoktorZiyaretMap());
            modelBuilder.Configurations.Add(new EgitimMap());
            modelBuilder.Configurations.Add(new FirmaMap());
            modelBuilder.Configurations.Add(new FirmaDoktorMap());
            modelBuilder.Configurations.Add(new FirmaUzmanMap());
            modelBuilder.Configurations.Add(new FirmaYapilacakMap());
            modelBuilder.Configurations.Add(new IlMap());
            modelBuilder.Configurations.Add(new IlceMap());
            modelBuilder.Configurations.Add(new IsyeriBolumuMap());
            modelBuilder.Configurations.Add(new ItemMap());
            modelBuilder.Configurations.Add(new MusteriMap());
            modelBuilder.Configurations.Add(new OlayYeriFotoMap());
            modelBuilder.Configurations.Add(new SaglikRaporuMap());
            modelBuilder.Configurations.Add(new SaglikRaporuTetkikMap());
            modelBuilder.Configurations.Add(new SahaDenetimMap());
            modelBuilder.Configurations.Add(new SektorMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new UzmanMap());
            modelBuilder.Configurations.Add(new YoneticiMap());
        }
    }
}
