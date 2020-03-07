namespace mertaksublogg.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class mertaksubloggDB : DbContext
    {
        public mertaksubloggDB()
            : base("name=mertaksubloggDB")
        {
        }

        public virtual DbSet<kategori> kategoris { get; set; }
        public virtual DbSet<makale> makales { get; set; }
        public virtual DbSet<uye> uyes { get; set; }
        public virtual DbSet<yetki> yetkis { get; set; }
        public virtual DbSet<yorum> yorums { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
