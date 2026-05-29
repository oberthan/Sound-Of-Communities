using System.Windows.Input;
using MeshWave.Core.Configuration;
using MeshWave.Core.Interfaces;
using MeshWave.Wpf.Mvvm;

namespace MeshWave.Wpf.ViewModels;

public class MainViewModel : ViewModelBase
{
    private object _currentView;
    private readonly AppConfig _config;
    private readonly ILibraryManager _libraryManager;

    public MainViewModel(ILibraryManager libraryManager, AppConfig config)
    {
        _libraryManager = libraryManager;
        _config = config;

        // Default view
        _currentView = new SetupViewModel(_config, _libraryManager);

        NavigateSetupCommand = new RelayCommand(_ => CurrentView = new SetupViewModel(_config, _libraryManager));
        NavigateLibraryCommand = new RelayCommand(_ => CurrentView = new LibraryViewModel(_libraryManager, _config));
        NavigateManageCommand = new RelayCommand(_ => CurrentView = new ManageMusicViewModel(_libraryManager, _config));
        NavigatePlayerCommand = new RelayCommand(_ => CurrentView = new PlayerViewModel(_libraryManager));
    }

    public object CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value);
    }

    public ICommand NavigateSetupCommand { get; }
    public ICommand NavigateLibraryCommand { get; }
    public ICommand NavigateManageCommand { get; }
    public ICommand NavigatePlayerCommand { get; }
}
