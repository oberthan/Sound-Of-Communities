using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MeshWave.Core.Interfaces;
using MeshWave.Core.Models;

namespace MeshWave.LibraryManager;

public class LibraryManager : ILibraryManager
{
    private string? _storagePath;
    private readonly List<Track> _tracks = new();
    private readonly List<Album> _albums = new();
    private readonly List<Comment> _comments = new();

    public async Task InitializeAsync(string storagePath)
    {
        _storagePath = storagePath;
        if (!string.IsNullOrEmpty(_storagePath))
        {
            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
            await LoadMetadataAsync();
        }
    }

    private async Task SaveMetadataAsync()
    {
        if (string.IsNullOrEmpty(_storagePath)) return;
        var metadataPath = Path.Combine(_storagePath, "metadata.json");
        var data = new { Tracks = _tracks, Albums = _albums, Comments = _comments };
        var json = JsonSerializer.Serialize(data);
        await File.WriteAllTextAsync(metadataPath, json);
    }

    private async Task LoadMetadataAsync()
    {
        if (string.IsNullOrEmpty(_storagePath)) return;
        var metadataPath = Path.Combine(_storagePath, "metadata.json");
        if (File.Exists(metadataPath))
        {
            var json = await File.ReadAllTextAsync(metadataPath);
            var data = JsonSerializer.Deserialize<MetadataContainer>(json);
            if (data != null)
            {
                _tracks.Clear(); _tracks.AddRange(data.Tracks);
                _albums.Clear(); _albums.AddRange(data.Albums);
                _comments.Clear(); _comments.AddRange(data.Comments);
            }
        }
    }

    private class MetadataContainer
    {
        public List<Track> Tracks { get; set; } = new();
        public List<Album> Albums { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
    }

    public async Task ScanForMusicAsync()
    {
        if (string.IsNullOrEmpty(_storagePath) || !Directory.Exists(_storagePath)) return;

        var files = Directory.EnumerateFiles(_storagePath, "*.*", SearchOption.AllDirectories)
            .Where(f => f.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".wav", StringComparison.OrdinalIgnoreCase));

        foreach (var file in files)
        {
            if (!_tracks.Any(t => t.FilePath == file))
            {
                var track = new Track
                {
                    Id = Guid.NewGuid(),
                    Title = Path.GetFileNameWithoutExtension(file),
                    FilePath = file,
                    ArtistId = Guid.Empty // Unknown artist from scan for now
                };
                await AddTrackAsync(track);
            }
        }
    }

    public Task<IEnumerable<Track>> GetAllTracksAsync()
    {
        return Task.FromResult(_tracks.AsEnumerable());
    }

    public Task<IEnumerable<Track>> GetLocalTracksAsync(Guid currentUserId)
    {
        return Task.FromResult(_tracks.Where(t => t.ArtistId == currentUserId));
    }

    public Task<IEnumerable<Track>> GetCommunityTracksAsync(Guid currentUserId)
    {
        return Task.FromResult(_tracks.Where(t => t.ArtistId != currentUserId));
    }

    public Task<IEnumerable<Album>> GetAllAlbumsAsync()
    {
        return Task.FromResult(_albums.AsEnumerable());
    }

    public Task<IEnumerable<Track>> GetTracksByArtistAsync(Guid artistId)
    {
        return Task.FromResult(_tracks.Where(t => t.ArtistId == artistId));
    }

    public async Task AddTrackAsync(Track track)
    {
        _tracks.Add(track);
        await SaveMetadataAsync();
    }

    public async Task AddAlbumAsync(Album album)
    {
        _albums.Add(album);
        await SaveMetadataAsync();
    }

    public Task<IEnumerable<Comment>> GetCommentsForTargetAsync(Guid targetId)
    {
        return Task.FromResult(_comments.Where(c => c.TargetId == targetId));
    }

    public async Task AddCommentAsync(Comment comment)
    {
        _comments.Add(comment);
        await SaveMetadataAsync();
    }

    public async Task DeleteCommentAsync(Guid commentId, Guid userId)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == commentId);
        if (comment != null)
        {
            _comments.Remove(comment);
            await SaveMetadataAsync();
        }
    }
}
