using System;
using System.Windows.Input;
using MeshWave.Core.Configuration;
using MeshWave.Core.Interfaces;
using MeshWave.Wpf.Mvvm;

namespace MeshWave.Wpf.ViewModels;

public class SetupViewModel : ViewModelBase
{
    private string _userName = "Artist Name";
    private string _storagePath = string.Empty;
    private readonly AppConfig _config;
    private readonly ILibraryManager? _libraryManager;

    public SetupViewModel(AppConfig config, ILibraryManager? libraryManager = null)
    {
        _config = config;
        _libraryManager = libraryManager;
        _userName = config.UserName;
        _storagePath = config.StorageRootPath;

        BrowseCommand = new RelayCommand(_ => Browse());
        SaveCommand = new RelayCommand(_ => Save());
    }

    public string UserName
    {
        get => _userName;
        set => SetProperty(ref _userName, value);
    }

    public string StoragePath
    {
        get => _storagePath;
        set => SetProperty(ref _storagePath, value);
    }

    public ICommand BrowseCommand { get; }
    public ICommand SaveCommand { get; }

    private void Browse()
    {
        // Use a mock folder selection for now as we don't have interactive UI in this environment
        // In a real WPF app, we'd use WinForms FolderBrowserDialog or a WPF equivalent.
        string defaultPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "MeshWave");
        StoragePath = defaultPath;
    }

    private async void Save()
    {
        _config.UserName = UserName;
        _config.StorageRootPath = StoragePath;
        if (_config.UserId == Guid.Empty)
        {
            _config.UserId = Guid.NewGuid();
        }

        if (_libraryManager != null)
        {
            await _libraryManager.InitializeAsync(_config.StorageRootPath);
            await _libraryManager.ScanForMusicAsync();
        }
        // Notify application to navigate or proceed
    }
}
