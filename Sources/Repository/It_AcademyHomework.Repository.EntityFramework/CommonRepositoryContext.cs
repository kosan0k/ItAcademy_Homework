using System;
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
        }
    }

}
