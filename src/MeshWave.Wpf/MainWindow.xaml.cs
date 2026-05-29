using System.Windows;
using MeshWave.Core.Configuration;
using MeshWave.LibraryManager;
using MeshWave.Wpf.ViewModels;

namespace MeshWave.Wpf;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Poor man's DI for now
        var config = AppConfig.Load();
        var libraryManager = new LibraryManager.LibraryManager();
        var playerService = new Services.MusicPlayerService();

        DataContext = new MainViewModel(libraryManager, config, playerService);

        if (!string.IsNullOrEmpty(config.StorageRootPath))
        {
            _ = libraryManager.InitializeAsync(config.StorageRootPath);
        }
    }
}
