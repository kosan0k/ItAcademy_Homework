using System.Collections.Generic;
using It_AcademyHomework.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace It_AcademyHomework.Repository.EntityFramework
{
    public class CommonRepositoryContext : DbContext
    {
        public CommonRepositoryContext()
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Catalog> Catalogs { get; set; }
        public virtual DbSet<Good> Goods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Catalog>()
                .HasMany(c => c.Goods);

            var catalogs = new List<Catalog>(3)
            {
                new Catalog() {Id = 1, Name = "Phones"},
                new Catalog() {Id = 2, Name = "Computers"},
                new Catalog() {Id = 3, Name = "Other"}
            };

            foreach (var catalog in catalogs)
            {
                modelBuilder.Entity<Catalog>().HasData(catalog);
            }

            modelBuilder.Entity<Good>().HasData(new Good[6]
            {
                new Good() { Id = 1, Name = "Xiaomi", Price = "230$", CatalogId = catalogs[0].Id},
                new Good() { Id = 2, Name = "Motorolla", Price = "130$", CatalogId = catalogs[0].Id},
                new Good() { Id = 3, Name = "AMD", Price = "1230$", CatalogId = catalogs[1].Id},
                new Good() { Id = 4, Name = "Intel", Price = "1130$", CatalogId = catalogs[1].Id},
                new Good() { Id = 5, Name = "Ethernet switcher", Price = "30$", CatalogId = catalogs[2].Id},
                new Good() { Id = 6, Name = "Modem", Price = "50$", CatalogId = catalogs[2].Id},
            });
        }
    }

}
