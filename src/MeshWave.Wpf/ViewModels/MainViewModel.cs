using System.Windows.Input;
using MeshWave.Core.Configuration;
using MeshWave.Core.Interfaces;
using MeshWave.Wpf.Mvvm;
using MeshWave.Wpf.Services;

namespace MeshWave.Wpf.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly ILibraryManager _libraryManager;
    private readonly AppConfig _config;

    public MainViewModel(
        INavigationService navigationService,
        ILibraryManager libraryManager,
        AppConfig config,
        IMusicPlayerService playerService,
        IWaveformService waveformService)
    {
        _navigationService = navigationService;
        _libraryManager = libraryManager;
        _config = config;
        Player = new PlayerViewModel(_libraryManager, playerService, waveformService);

        _navigationService.CurrentViewModelChanged += () => OnPropertyChanged(nameof(CurrentView));

        NavigateLibraryCommand = new RelayCommand(_ => _navigationService.NavigateTo<LibraryViewModel>());
        NavigateManageCommand = new RelayCommand(_ => _navigationService.NavigateTo<ManageMusicViewModel>());
        NavigateSyncCommand = new RelayCommand(_ => _navigationService.NavigateTo<SyncStatusViewModel>());
        NavigateSetupCommand = new RelayCommand(_ => _navigationService.NavigateTo<SetupViewModel>());

        // Initial navigation
        _navigationService.NavigateTo<LibraryViewModel>();

        if (!string.IsNullOrEmpty(_config.StorageRootPath))
        {
            _ = _libraryManager.InitializeAsync(_config.StorageRootPath);
        }
    }

    public object? CurrentView => _navigationService.CurrentViewModel;

    public PlayerViewModel Player { get; }

    public ICommand NavigateLibraryCommand { get; }
    public ICommand NavigateManageCommand { get; }
    public ICommand NavigateSyncCommand { get; }
    public ICommand NavigateSetupCommand { get; }
}
