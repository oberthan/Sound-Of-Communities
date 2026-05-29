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
        var config = new AppConfig();
        var libraryManager = new LibraryManager.LibraryManager();

        DataContext = new MainViewModel(libraryManager, config);

        if (!string.IsNullOrEmpty(config.StorageRootPath))
        {
            _ = libraryManager.InitializeAsync(config.StorageRootPath);
        }
    }
}
