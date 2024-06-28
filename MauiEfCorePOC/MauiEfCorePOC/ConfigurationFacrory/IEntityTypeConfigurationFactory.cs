using System;
using Microsoft.EntityFrameworkCore;

namespace MauiEfCorePOC.ConfigurationFacrory
{
    public interface IEntityTypeConfigurationFactory
    {
        IEntityTypeConfiguration<TEntity>? GetConfiguration<TEntity>() where TEntity : class;
        object? GetConfiguration(Type entityType);
    }
}

