using System;
using System.Linq;
using System.Threading.Tasks;
using MeshWave.Core.Models;
using MeshWave.LibraryManager;
using Xunit;

namespace MeshWave.Tests;

public class LibraryManagerTests
{
    [Fact]
    public async Task AddTrack_ShouldIncludeInGetAllTracks()
    {
        // Arrange
        var libraryManager = new LibraryManager.LibraryManager();
        var track = new Track { Id = Guid.NewGuid(), Title = "Test Track", ArtistId = Guid.NewGuid() };

        // Act
        await libraryManager.AddTrackAsync(track);
        var tracks = await libraryManager.GetAllTracksAsync();

        // Assert
        Assert.Contains(track, tracks);
    }

    [Fact]
    public async Task GetLocalTracks_ShouldReturnOnlyUserTracks()
    {
        // Arrange
        var libraryManager = new LibraryManager.LibraryManager();
        var userId = Guid.NewGuid();
        var otherId = Guid.NewGuid();
        var localTrack = new Track { Id = Guid.NewGuid(), ArtistId = userId, Title = "Local" };
        var communityTrack = new Track { Id = Guid.NewGuid(), ArtistId = otherId, Title = "Community" };

        await libraryManager.AddTrackAsync(localTrack);
        await libraryManager.AddTrackAsync(communityTrack);

        // Act
        var localTracks = await libraryManager.GetLocalTracksAsync(userId);

        // Assert
        Assert.Single(localTracks);
        Assert.Equal(localTrack.Id, localTracks.First().Id);
    }
}
