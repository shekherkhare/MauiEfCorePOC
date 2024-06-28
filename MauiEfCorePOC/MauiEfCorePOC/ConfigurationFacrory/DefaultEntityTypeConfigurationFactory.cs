using System;
using Microsoft.EntityFrameworkCore;

namespace MauiEfCorePOC.ConfigurationFacrory
{
    public class DefaultEntityTypeConfigurationFactory : IEntityTypeConfigurationFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultEntityTypeConfigurationFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEntityTypeConfiguration<TEntity>? GetConfiguration<TEntity>() where TEntity : class
        {
            return _serviceProvider.GetService<IEntityTypeConfiguration<TEntity>>();
        }

        private static readonly Type EntityTypeConfigurationDefinition = typeof(IEntityTypeConfiguration<>);

        public object? GetConfiguration(Type entityType)
        {
            var configurationType = EntityTypeConfigurationDefinition.MakeGenericType(entityType);
            return _serviceProvider.GetService(configurationType);
        }
    }
}

