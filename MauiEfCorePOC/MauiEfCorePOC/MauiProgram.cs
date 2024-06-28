using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Logging;
using MauiEfCorePOC.Context;
using MauiEfCorePOC.ConfigurationFacrory;
using MauiEfCorePOC.Extensions;
using MauiEfCorePOC.ValueComparer;

namespace MauiEfCorePOC;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        // Add EF Core and configurations
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db");
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));


        // Register MainPage with DI
        builder.Services.AddTransient<MainPage>();

        builder.Services.AddSingleton<ByteArrayValueComparer>();

        builder.Services.AddSingleton<IEntityTypeConfigurationFactory, DefaultEntityTypeConfigurationFactory>();
        builder.Services.ScanEntityTypeConfigurations(Assembly.GetExecutingAssembly());

        return builder.Build();
    }
}

