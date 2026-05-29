using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MeshWave.Wpf.Services;
using TagLib;
using System.Windows.Input;
using MeshWave.Core.Configuration;
using MeshWave.Core.Interfaces;
using MeshWave.Core.Models;
using MeshWave.Wpf.Mvvm;

namespace MeshWave.Wpf.ViewModels;

public class ManageMusicViewModel : ViewModelBase
{
    private readonly ILibraryManager _libraryManager;
    private readonly AppConfig _config;
    private readonly IMusicPlayerService _playerService;

    public ManageMusicViewModel(ILibraryManager libraryManager, AppConfig config, IMusicPlayerService playerService)
    {
        _libraryManager = libraryManager;
        _config = config;
        _playerService = playerService;
        MyTracks = new ObservableCollection<Track>();
        PlayCommand = new RelayCommand(p => _playerService.Play((Track)p!));
        AddFileCommand = new RelayCommand(_ => AddFile());
        AddFolderCommand = new RelayCommand(_ => AddFolder());

        _ = LoadMyTracksAsync();
    }

    public ObservableCollection<Track> MyTracks { get; }

    public ICommand PlayCommand { get; }
    public ICommand AddFileCommand { get; }
    public ICommand AddFolderCommand { get; }

    private async Task LoadMyTracksAsync()
    {
        var tracks = await _libraryManager.GetLocalTracksAsync(_config.UserId);
        MyTracks.Clear();
        foreach (var track in tracks)
        {
            MyTracks.Add(track);
        }
    }

    private async void AddFile()
    {
        var dialog = new Microsoft.Win32.OpenFileDialog
        {
            Filter = "Audio files|*.mp3;*.wav;*.flac;*.m4a",
            Multiselect = true
        };

        if (dialog.ShowDialog() == true)
        {
            foreach (var fileName in dialog.FileNames)
            {
                await ProcessNewTrack(fileName);
            }
        }
    }

    private async void AddFolder()
    {
        var dialog = new Microsoft.Win32.OpenFolderDialog();
        if (dialog.ShowDialog() == true)
        {
            var extensions = new[] { ".mp3", ".wav", ".flac", ".m4a" };
            var files = System.IO.Directory.EnumerateFiles(dialog.FolderName, "*.*", System.IO.SearchOption.AllDirectories)
                .Where(f => extensions.Contains(System.IO.Path.GetExtension(f).ToLower()));

            foreach (var file in files)
            {
                await ProcessNewTrack(file);
            }
        }
    }

    private async Task ProcessNewTrack(string filePath)
    {
        var userFolder = _config.GetUserStoragePath();
        if (!System.IO.Directory.Exists(userFolder)) System.IO.Directory.CreateDirectory(userFolder);

        var destPath = System.IO.Path.Combine(userFolder, System.IO.Path.GetFileName(filePath));
        if (filePath != destPath)
        {
            try { System.IO.File.Copy(filePath, destPath, true); } catch { /* Permission or locking issue */ }
        }

        var track = await _libraryManager.TryAddTrackFromFileAsync(destPath, _config.UserId);
        if (!MyTracks.Any(t => t.FilePath == track.FilePath))
        {
            MyTracks.Add(track);
        }
    }
}
