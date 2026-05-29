using System.Collections.Generic;
using System.Threading.Tasks;
using MeshWave.Core.Models;

namespace MeshWave.Core.Interfaces;

public interface ILibraryManager
{
    Task<IEnumerable<Track>> GetAllTracksAsync();
    Task<IEnumerable<Track>> GetLocalTracksAsync(Guid currentUserId);
    Task<IEnumerable<Track>> GetCommunityTracksAsync(Guid currentUserId);
    Task<IEnumerable<Album>> GetAllAlbumsAsync();
    Task<IEnumerable<Track>> GetTracksByArtistAsync(Guid artistId);
    Task<Track> TryAddTrackFromFileAsync(string filePath, Guid artistId);
    Task AddTrackAsync(Track track);
    Task AddAlbumAsync(Album album);
    Task<IEnumerable<Comment>> GetCommentsForTargetAsync(Guid targetId);
    Task AddCommentAsync(Comment comment);
    Task DeleteCommentAsync(Guid commentId, Guid userId);
    Task RemoveTrackAsync(Guid trackId);
    Task InitializeAsync(string storagePath);
    Task ScanForMusicAsync();
}

public interface ISynchronizer
{
    Task StartAsync();
    Task StopAsync();
    Task SynchronizeAsync();
    Task BroadcastUpdateAsync<T>(T item) where T : class;
    bool ValidateOwnership(Guid entityId, Guid userId, Guid actualOwnerId);
    IEnumerable<string> ConnectedPeers { get; }
    int ActiveDownloads { get; }
    int ActiveUploads { get; }
    event Action? NetworkStatusChanged;
}
