using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Threading;
using MeshWave.Core.Models;

namespace MeshWave.Wpf.Services;

public interface IMusicPlayerService
{
    Track? CurrentTrack { get; }
    bool IsPlaying { get; }
    double Volume { get; set; }
    TimeSpan CurrentPosition { get; set; }
    TimeSpan TotalDuration { get; }

    void Play(Track track);
    void Pause();
    void Resume();
    void Stop();

    event Action? PlayStateChanged;
    event Action? PositionChanged;
    event Action? TrackEnded;
}

public class MusicPlayerService : IMusicPlayerService
{
    private readonly MediaPlayer _mediaPlayer = new();
    private readonly DispatcherTimer _timer;
    private Track? _currentTrack;
    private bool _isPlaying;

    public MusicPlayerService()
    {
        _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(200) };
        _timer.Tick += (s, e) => PositionChanged?.Invoke();

        _mediaPlayer.MediaOpened += (s, e) => PlayStateChanged?.Invoke();
        _mediaPlayer.MediaEnded += (s, e) => {
            _isPlaying = false;
            _timer.Stop();
            TrackEnded?.Invoke();
            PlayStateChanged?.Invoke();
        };
    }

    public Track? CurrentTrack => _currentTrack;
    public bool IsPlaying => _isPlaying;

    public double Volume
    {
        get => _mediaPlayer.Volume;
        set => _mediaPlayer.Volume = value;
    }

    public void Play(Track track)
    {
        if (_currentTrack?.FilePath != track.FilePath)
        {
            _currentTrack = track;
            _mediaPlayer.Open(new Uri(track.FilePath));
        }

        _mediaPlayer.Play();
        _isPlaying = true;
        _timer.Start();
        PlayStateChanged?.Invoke();
    }

    public void Pause()
    {
        _mediaPlayer.Pause();
        _isPlaying = false;
        _timer.Stop();
        PlayStateChanged?.Invoke();
    }

    public void Resume()
    {
        if (_currentTrack != null)
        {
            _mediaPlayer.Play();
            _isPlaying = true;
            _timer.Start();
            PlayStateChanged?.Invoke();
        }
    }

    public void Stop()
    {
        _mediaPlayer.Stop();
        _isPlaying = false;
        _timer.Stop();
        PlayStateChanged?.Invoke();
    }

    public TimeSpan CurrentPosition
    {
        get => _mediaPlayer.Position;
        set => _mediaPlayer.Position = value;
    }

    public TimeSpan TotalDuration => _mediaPlayer.NaturalDuration.HasTimeSpan ? _mediaPlayer.NaturalDuration.TimeSpan : TimeSpan.Zero;

    public event Action? PlayStateChanged;
    public event Action? PositionChanged;
    public event Action? TrackEnded;
}
