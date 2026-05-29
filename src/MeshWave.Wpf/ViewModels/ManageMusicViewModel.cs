using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

    public ManageMusicViewModel(ILibraryManager libraryManager, AppConfig config)
    {
        _libraryManager = libraryManager;
        _config = config;
        MyTracks = new ObservableCollection<Track>();
        AddTrackCommand = new RelayCommand(_ => AddTrack());

        _ = LoadMyTracksAsync();
    }

    public ObservableCollection<Track> MyTracks { get; }

    public ICommand AddTrackCommand { get; }

    private async Task LoadMyTracksAsync()
    {
        var tracks = await _libraryManager.GetLocalTracksAsync(_config.UserId);
        MyTracks.Clear();
        foreach (var track in tracks)
        {
            MyTracks.Add(track);
        }
    }

    private void AddTrack()
    {
        // Mock adding a track
        var newTrack = new Track
        {
            Id = Guid.NewGuid(),
            ArtistId = _config.UserId,
            Title = "New Track",
            FilePath = "C:\\Music\\new_track.mp3"
        };
        _libraryManager.AddTrackAsync(newTrack);
        MyTracks.Add(newTrack);
    }
}
