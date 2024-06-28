using System;
using MauiEfCorePOC.ConfigurationFacrory;
using MauiEfCorePOC.Entities;
using MauiEfCorePOC.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MauiEfCorePOC.Context
{
    public class AppDbContext : DbContext
    {
        private readonly IEntityTypeConfigurationFactory _entityTypeConfigurationFactory;

        public AppDbContext(DbContextOptions<AppDbContext> options, IEntityTypeConfigurationFactory entityTypeConfigurationFactory)
            : base(options)
        {
            _entityTypeConfigurationFactory = entityTypeConfigurationFactory;
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations(_entityTypeConfigurationFactory);
        }
    }

}

