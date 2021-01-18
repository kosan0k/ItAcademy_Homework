using System;
using Microsoft.EntityFrameworkCore;

namespace It_AcademyHomework.Repository.EntityFramework
{
    public class InMemoryRepositoryContext : CommonRepositoryContext
    {
        private const string _dbName = "CustomDbName";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(_dbName);
        }
    }
}
