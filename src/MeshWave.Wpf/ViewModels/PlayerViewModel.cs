using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MeshWave.Core.Interfaces;
using MeshWave.Core.Models;
using MeshWave.Wpf.Mvvm;
using MeshWave.Wpf.Services;

namespace MeshWave.Wpf.ViewModels;

public class PlayerViewModel : ViewModelBase
{
    private readonly ILibraryManager _libraryManager;
    private readonly IMusicPlayerService _playerService;
    private readonly IWaveformService _waveformService;
    private Track? _currentTrack;
    private double _progress;
    private double _volume = 0.5;
    private ObservableCollection<float> _waveformPoints = new();
    private string _newCommentText = string.Empty;

    public PlayerViewModel(ILibraryManager libraryManager, IMusicPlayerService playerService, IWaveformService waveformService)
    {
        _libraryManager = libraryManager;
        _playerService = playerService;
        _waveformService = waveformService;
        Comments = new ObservableCollection<Comment>();

        PlayPauseCommand = new RelayCommand(_ => PlayPause());
        PostCommentCommand = new RelayCommand(_ => PostComment(), _ => !string.IsNullOrWhiteSpace(NewCommentText) && CurrentTrack != null);

        _playerService.PlayStateChanged += () => {
            CurrentTrack = _playerService.CurrentTrack;
            OnPropertyChanged(nameof(IsPlaying));
            OnPropertyChanged(nameof(PlayPauseIcon));
            OnPropertyChanged(nameof(PlayPauseActionText));
            OnPropertyChanged(nameof(DurationStr));
        };

        _playerService.PositionChanged += () => {
            OnPropertyChanged(nameof(CurrentTimeStr));
            UpdateProgress();
        };

        _playerService.Volume = _volume;
    }

    public Track? CurrentTrack
    {
        get => _currentTrack;
        set
        {
            if (SetProperty(ref _currentTrack, value))
            {
                _ = UpdateWaveformAsync();
            }
        }
    }

    public ObservableCollection<float> WaveformPoints => _waveformPoints;

    private async Task UpdateWaveformAsync()
    {
        if (CurrentTrack != null)
        {
            var data = await _waveformService.GetWaveformDataAsync(CurrentTrack.FilePath, 100);
            WaveformPoints.Clear();
            foreach (var p in data) WaveformPoints.Add(p);
        }
    }

    public bool IsPlaying => _playerService.IsPlaying;

    public string PlayPauseIcon => IsPlaying ? "⏸" : "▶";
    public string PlayPauseActionText => IsPlaying ? "Pause" : "Play";

    public string CurrentTimeStr => _playerService.CurrentPosition.ToString(@"mm\:ss");
    public string DurationStr => _playerService.TotalDuration.ToString(@"mm\:ss");

    public double Progress
    {
        get => _progress;
        set
        {
            if (SetProperty(ref _progress, value))
            {
                // Seek logic if needed
            }
        }
    }

    public double Volume
    {
        get => _volume;
        set
        {
            if (SetProperty(ref _volume, value))
            {
                _playerService.Volume = value;
            }
        }
    }

    public string NewCommentText
    {
        get => _newCommentText;
        set
        {
            if (SetProperty(ref _newCommentText, value))
            {
                ((RelayCommand)PostCommentCommand).NotifyCanExecuteChanged();
            }
        }
    }

    public ObservableCollection<Comment> Comments { get; }

    public ICommand PlayPauseCommand { get; }
    public ICommand PostCommentCommand { get; }

    private void PlayPause()
    {
        if (IsPlaying)
            _playerService.Pause();
        else
            _playerService.Resume();
    }

    private async void PostComment()
    {
        if (CurrentTrack == null) return;
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            TargetId = CurrentTrack.Id,
            Content = NewCommentText,
            TrackTimestamp = _playerService.CurrentPosition,
            CreatedAt = DateTime.Now
        };
        await _libraryManager.AddCommentAsync(comment);
        Comments.Add(comment);
        NewCommentText = string.Empty;
    }

    private void UpdateProgress()
    {
        var total = _playerService.TotalDuration.TotalSeconds;
        if (total > 0)
        {
            _progress = (_playerService.CurrentPosition.TotalSeconds / total) * 100;
            OnPropertyChanged(nameof(Progress));
        }
    }
}
