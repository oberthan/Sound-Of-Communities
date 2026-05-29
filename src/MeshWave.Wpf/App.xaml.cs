using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using MeshWave.Core.Interfaces;
using MeshWave.Core.Configuration;
using MeshWave.LibraryManager;
using MeshWave.Wpf.Services;
using MeshWave.Wpf.ViewModels;

namespace MeshWave.Wpf;

public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Core and Infrastructure
        services.AddSingleton(AppConfig.Load());
        services.AddSingleton<ILibraryManager, MeshWave.LibraryManager.LibraryManager>();
        services.AddSingleton<ISynchronizer, MeshWave.Synchronizer.Synchronizer>();

        // Services
        services.AddSingleton<IMusicPlayerService, MusicPlayerService>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IWaveformService, WaveformService>();

        // ViewModels
        services.AddSingleton<MainViewModel>();
        services.AddTransient<LibraryViewModel>();
        services.AddTransient<ManageMusicViewModel>();
        services.AddTransient<PlayerViewModel>();
        services.AddTransient<SyncStatusViewModel>();
        services.AddTransient<SetupViewModel>();

        // Windows
        services.AddSingleton<MainWindow>(s => new MainWindow
        {
            DataContext = s.GetRequiredService<MainViewModel>()
        });
    }
}
