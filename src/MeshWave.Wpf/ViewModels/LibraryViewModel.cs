using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MeshWave.Core.Configuration;
using MeshWave.Core.Interfaces;
using MeshWave.Core.Models;
using MeshWave.Wpf.Mvvm;

namespace MeshWave.Wpf.ViewModels;

public class LibraryViewModel : ViewModelBase
{
    private readonly ILibraryManager _libraryManager;
    private readonly AppConfig _config;

    public LibraryViewModel(ILibraryManager libraryManager, AppConfig config)
    {
        _libraryManager = libraryManager;
        _config = config;
        CommunityTracks = new ObservableCollection<Track>();
        PlayCommand = new RelayCommand(p => PlayTrack(p as Track));

        _ = LoadTracksAsync();
    }

    public ObservableCollection<Track> CommunityTracks { get; }

    public ICommand PlayCommand { get; }

    private async Task LoadTracksAsync()
    {
        var tracks = await _libraryManager.GetCommunityTracksAsync(_config.UserId);
        CommunityTracks.Clear();
        foreach (var track in tracks)
        {
            CommunityTracks.Add(track);
        }
    }

    private void PlayTrack(Track? track)
    {
        if (track == null) return;
        // Signal player to play
    }
}
