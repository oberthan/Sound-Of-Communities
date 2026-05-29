using System;
using System.Windows.Input;
using MeshWave.Core.Configuration;
using MeshWave.Core.Interfaces;
using MeshWave.Wpf.Mvvm;
using MeshWave.Wpf.Services;

namespace MeshWave.Wpf.ViewModels;

public class MainViewModel : ViewModelBase
{
    private object _currentView;
    private readonly AppConfig _config;
    private readonly ILibraryManager _libraryManager;
    private readonly IMusicPlayerService _playerService;

    public MainViewModel(ILibraryManager libraryManager, AppConfig config, IMusicPlayerService playerService)
    {
        _libraryManager = libraryManager;
        _config = config;
        _playerService = playerService;

        // Default view is now Library
        _currentView = new LibraryViewModel(_libraryManager, _config, _playerService);

        NavigateLibraryCommand = new RelayCommand(_ => CurrentView = new LibraryViewModel(_libraryManager, _config, _playerService));
        NavigateManageCommand = new RelayCommand(_ => CurrentView = new ManageMusicViewModel(_libraryManager, _config, _playerService));
        NavigatePlayerCommand = new RelayCommand(_ => CurrentView = new PlayerViewModel(_libraryManager, _playerService));
        NavigateSyncCommand = new RelayCommand(_ => CurrentView = new SyncStatusViewModel());
        NavigateSetupCommand = new RelayCommand(_ => CurrentView = new SetupViewModel(_config, _libraryManager));
    }

    public object CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value);
    }

    public ICommand NavigateLibraryCommand { get; }
    public ICommand NavigateManageCommand { get; }
    public ICommand NavigatePlayerCommand { get; }
    public ICommand NavigateSyncCommand { get; }
    public ICommand NavigateSetupCommand { get; }
}
