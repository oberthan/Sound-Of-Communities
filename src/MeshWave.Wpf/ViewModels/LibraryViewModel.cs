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
    private List<Track> _allTracks = new();
    private bool _showLocalOnly;

    public LibraryViewModel(ILibraryManager libraryManager, AppConfig config, IMusicPlayerService playerService)
    {
        _libraryManager = libraryManager;
        _config = config;
        _playerService = playerService;
        Tracks = new ObservableCollection<Track>();
        PlayCommand = new RelayCommand(p => PlayTrack(p as Track));
        ToggleLocalCommand = new RelayCommand(_ => { ShowLocalOnly = !ShowLocalOnly; });
        RemoveTrackCommand = new RelayCommand(_ => { });
        ClearSearchCommand = new RelayCommand(_ => SearchText = string.Empty);

        _ = LoadTracksAsync();
    }

    public ObservableCollection<Track> Tracks { get; }

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

    public bool ShowLocalOnly
    {
        get => _showLocalOnly;
        set
        {
            if (SetProperty(ref _showLocalOnly, value))
            {
                FilterTracks();
            }
        }
    }

    public ICommand PlayCommand { get; }
    public ICommand ToggleLocalCommand { get; }
    public ICommand RemoveTrackCommand { get; }
    public ICommand ClearSearchCommand { get; }

    private async Task LoadTracksAsync()
    {
        var tracks = await _libraryManager.GetAllTracksAsync();
        _allTracks = tracks.ToList();
        FilterTracks();
    }

    private void FilterTracks()
    {
        var filtered = _allTracks.Where(t =>
            (string.IsNullOrEmpty(SearchText) || t.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) &&
            (!ShowLocalOnly || t.ArtistId == _config.UserId));

        Tracks.Clear();
        foreach (var track in filtered)
        {
            Tracks.Add(track);
        }
    }

    private void PlayTrack(Track? track)
    {
        if (track == null) return;
        _playerService.Play(track);
    }
}
