using System;
using System.Windows.Media;
using MeshWave.Core.Models;

namespace MeshWave.Wpf.Services;

public interface IMusicPlayerService
{
    Track? CurrentTrack { get; }
    void Play(Track track);
    void Pause();
    void Resume();
    TimeSpan CurrentPosition { get; set; }
    TimeSpan TotalDuration { get; }
    event Action PlayStateChanged;
    event Action PositionChanged;
}

public class MusicPlayerService : IMusicPlayerService
{
    private readonly MediaPlayer _mediaPlayer = new();
    private Track? _currentTrack;
    private System.Windows.Threading.DispatcherTimer _timer;

    public MusicPlayerService()
    {
        _timer = new System.Windows.Threading.DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
        _timer.Tick += (s, e) => PositionChanged?.Invoke();
    }

    public Track? CurrentTrack => _currentTrack;

    public void Play(Track track)
    {
        _currentTrack = track;
        _mediaPlayer.Open(new Uri(track.FilePath));
        _mediaPlayer.Play();
        _timer.Start();
        PlayStateChanged?.Invoke();
    }

    public void Pause()
    {
        _mediaPlayer.Pause();
        _timer.Stop();
        PlayStateChanged?.Invoke();
    }

    public void Resume()
    {
        _mediaPlayer.Play();
        _timer.Start();
        PlayStateChanged?.Invoke();
    }

    public TimeSpan CurrentPosition
    {
        get => _mediaPlayer.Position;
        set => _mediaPlayer.Position = value;
    }

    public TimeSpan TotalDuration => _mediaPlayer.NaturalDuration.HasTimeSpan ? _mediaPlayer.NaturalDuration.TimeSpan : TimeSpan.Zero;

    public event Action PlayStateChanged;
    public event Action PositionChanged;
}
