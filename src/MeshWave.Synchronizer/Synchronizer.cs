using System;
using System.Threading.Tasks;
using MeshWave.Core.Interfaces;
using MeshWave.Core.Models;

namespace MeshWave.Synchronizer;

public class Synchronizer : ISynchronizer
{
    private readonly P2PService _p2pService = new();

    public IEnumerable<string> ConnectedPeers => _p2pService.DiscoveredPeers;
    public int ActiveDownloads => 0;
    public int ActiveUploads => 0;
    public event Action? NetworkStatusChanged;

    public Synchronizer()
    {
        _p2pService.PeersChanged += () => NetworkStatusChanged?.Invoke();
    }

    public Task StartAsync()
    {
        try { _p2pService.StartDiscovery(); } catch { /* Port busy etc */ }
        return Task.CompletedTask;
    }

    public Task StopAsync()
    {
        // Stub for stopping network services
        return Task.CompletedTask;
    }

    public Task SynchronizeAsync()
    {
        // Stub for performing exchange of music and comment files
        return Task.CompletedTask;
    }

    public Task BroadcastUpdateAsync<T>(T item) where T : class
    {
        // Stub for broadcasting updates to the mesh network
        return Task.CompletedTask;
    }

    public bool ValidateOwnership(Guid entityId, Guid userId, Guid actualOwnerId)
    {
        // In a real blockchain system, this would verify signatures.
        // For this implementation, we compare the userId with the actualOwnerId of the entity.
        return userId == actualOwnerId;
    }

    public bool CanDeleteComment(Comment comment, Guid userId, Guid musicOwnerId)
    {
        // "Any user can add or edit own comments to other users music, but only the owner can delete comments."
        // Interpret "owner" as the music owner (artist) based on typical requirements for such apps.
        return userId == musicOwnerId;
    }
}
