using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MeshWave.Core.Interfaces;
using MeshWave.Core.Models;
using MeshWave.Wpf.Mvvm;

namespace MeshWave.Wpf.ViewModels;

public class PlayerViewModel : ViewModelBase
{
    private readonly ILibraryManager _libraryManager;
    private Track? _currentTrack;
    private bool _isPlaying;
    private TimeSpan _currentTime;
    private double _progress;

    public PlayerViewModel(ILibraryManager libraryManager)
    {
        _libraryManager = libraryManager;
        Comments = new ObservableCollection<Comment>();
        PlayPauseCommand = new RelayCommand(_ => PlayPause());
        AddCommentCommand = new RelayCommand(_ => AddComment());
    }

    public Track? CurrentTrack
    {
        get => _currentTrack;
        set
        {
            if (SetProperty(ref _currentTrack, value))
            {
                _ = LoadCommentsAsync();
            }
        }
    }

    public bool IsPlaying
    {
        get => _isPlaying;
        set => SetProperty(ref _isPlaying, value);
    }

    public TimeSpan CurrentTime
    {
        get => _currentTime;
        set
        {
            if (SetProperty(ref _currentTime, value))
            {
                UpdateProgress();
            }
        }
    }

    public double Progress
    {
        get => _progress;
        set => SetProperty(ref _progress, value);
    }

    public ObservableCollection<Comment> Comments { get; }

    public ICommand PlayPauseCommand { get; }
    public ICommand AddCommentCommand { get; }

    private void PlayPause()
    {
        IsPlaying = !IsPlaying;
    }

    private async Task LoadCommentsAsync()
    {
        if (CurrentTrack == null) return;
        var comments = await _libraryManager.GetCommentsForTargetAsync(CurrentTrack.Id);
        Comments.Clear();
        foreach (var comment in comments)
        {
            Comments.Add(comment);
        }
    }

    private void AddComment()
    {
        if (CurrentTrack == null) return;
        // Mock adding comment at current time
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            TargetId = CurrentTrack.Id,
            Content = "Cool bridge!",
            TrackTimestamp = CurrentTime,
            CreatedAt = DateTime.Now
        };
        _libraryManager.AddCommentAsync(comment);
        Comments.Add(comment);
    }

    private void UpdateProgress()
    {
        if (CurrentTrack == null || CurrentTrack.Duration == TimeSpan.Zero)
        {
            Progress = 0;
        }
        else
        {
            Progress = CurrentTime.TotalSeconds / CurrentTrack.Duration.TotalSeconds * 100;
        }
    }
}
