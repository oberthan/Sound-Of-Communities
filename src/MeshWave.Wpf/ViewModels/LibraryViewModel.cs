using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MeshWave.Core.Configuration;
using MeshWave.Core.Interfaces;
using MeshWave.Core.Models;
using MeshWave.Wpf.Mvvm;

namespace MeshWave.Wpf.ViewModels;

using MeshWave.Wpf.Services;
using System.Linq;
using System.Collections.Generic;

public class LibraryViewModel : ViewModelBase
{
    private readonly ILibraryManager _libraryManager;
    private readonly AppConfig _config;
    private readonly IMusicPlayerService _playerService;
    private string _searchText = string.Empty;
    private List<Track> _allCommunityTracks = new();

    public LibraryViewModel(ILibraryManager libraryManager, AppConfig config, IMusicPlayerService playerService)
    {
        _libraryManager = libraryManager;
        _config = config;
        _playerService = playerService;
        CommunityTracks = new ObservableCollection<Track>();
        PlayCommand = new RelayCommand(p => PlayTrack(p as Track));

        _ = LoadTracksAsync();
    }

    public ObservableCollection<Track> CommunityTracks { get; }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (SetProperty(ref _searchText, value))
            {
                FilterTracks();
            }
        }
    }

    public ICommand PlayCommand { get; }

    private async Task LoadTracksAsync()
    {
        var tracks = await _libraryManager.GetCommunityTracksAsync(_config.UserId);
        _allCommunityTracks = tracks.ToList();
        FilterTracks();
    }

    private void FilterTracks()
    {
        var filtered = _allCommunityTracks.Where(t =>
            string.IsNullOrEmpty(SearchText) ||
            t.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        CommunityTracks.Clear();
        foreach (var track in filtered)
        {
            CommunityTracks.Add(track);
        }
    }

    private void PlayTrack(Track? track)
    {
        if (track == null) return;
        _playerService.Play(track);
    }
}
