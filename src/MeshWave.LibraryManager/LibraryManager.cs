using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TagLib;
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
            // Ensure community folder exists
            var communityPath = Path.Combine(_storagePath, "Community");
            if (!Directory.Exists(communityPath)) Directory.CreateDirectory(communityPath);

            await LoadMetadataAsync();
        }
    }

    private async Task SaveMetadataAsync()
    {
        if (string.IsNullOrEmpty(_storagePath)) return;
        var metadataPath = Path.Combine(_storagePath, "metadata.json");
        var data = new { Tracks = _tracks, Albums = _albums, Comments = _comments };
        var json = JsonSerializer.Serialize(data);
        await System.IO.File.WriteAllTextAsync(metadataPath, json);
    }

    private async Task LoadMetadataAsync()
    {
        if (string.IsNullOrEmpty(_storagePath)) return;
        var metadataPath = Path.Combine(_storagePath, "metadata.json");
        if (System.IO.File.Exists(metadataPath))
        {
            var json = await System.IO.File.ReadAllTextAsync(metadataPath);
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

        var extensions = new[] { ".mp3", ".wav", ".flac", ".m4a" };
        var files = Directory.EnumerateFiles(_storagePath, "*.*", SearchOption.AllDirectories)
            .Where(f => extensions.Contains(Path.GetExtension(f).ToLower()));

        foreach (var file in files)
        {
            if (!_tracks.Any(t => t.FilePath == file))
            {
                try
                {
                    using var tfile = TagLib.File.Create(file);
                    var track = new Track
                    {
                        Id = Guid.NewGuid(),
                        Title = string.IsNullOrEmpty(tfile.Tag.Title) ? Path.GetFileNameWithoutExtension(file) : tfile.Tag.Title,
                        FilePath = file,
                        Duration = tfile.Properties.Duration,
                        Description = tfile.Tag.Comment ?? string.Empty,
                        // For a real app, we would map the artist name to an ID or create a new user profile
                    };
                    await AddTrackAsync(track);
                }
                catch
                {
                    // Fallback for files with corrupt metadata
                    var track = new Track
                    {
                        Id = Guid.NewGuid(),
                        Title = Path.GetFileNameWithoutExtension(file),
                        FilePath = file,
                        ArtistId = Guid.Empty
                    };
                    await AddTrackAsync(track);
                }
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
