using System;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MauiEfCorePOC.ConfigurationFacrory;

namespace MauiEfCorePOC.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ScanEntityTypeConfigurations(this IServiceCollection services, Assembly assembly)
        {
            var configurationTypes = assembly.DefinedTypes
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();

            foreach (var type in configurationTypes)
            {
                var interfaceType = type.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
                services.AddSingleton(interfaceType, type);
            }

            return services;
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void ApplyAllConfigurations(this ModelBuilder modelBuilder, IEntityTypeConfigurationFactory entityTypeConfigurationFactory)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes().Select(e => e.ClrType);

            foreach (var entityType in entityTypes)
            {
                var configuration = entityTypeConfigurationFactory.GetConfiguration(entityType);
                if (configuration != null)
                {
                    var method = typeof(ModelBuilder).GetMethod(nameof(ModelBuilder.ApplyConfiguration))!
                        .MakeGenericMethod(entityType);
                    method.Invoke(modelBuilder, new[] { configuration });
                }
            }
        }
    }

}

