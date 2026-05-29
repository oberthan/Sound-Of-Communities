using System;
using System.Collections.Generic;

namespace MeshWave.Core.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? CoverImagePath { get; set; }
}

public class Album
{
    public Guid Id { get; set; }
    public Guid ArtistId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? CoverImagePath { get; set; }
    public List<Guid> TrackIds { get; set; } = new();
}

public class Track
{
    public Guid Id { get; set; }
    public Guid ArtistId { get; set; }
    public string ArtistName { get; set; } = "Unknown Artist";
    public Guid? AlbumId { get; set; }
    public string AlbumTitle { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? CoverImagePath { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public string Genre { get; set; } = string.Empty;
    public int? Year { get; set; }
}

public class Comment
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid TargetId { get; set; } // Can be AlbumId or TrackId
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public TimeSpan? TrackTimestamp { get; set; } // Specific time in the track
}

public class CommunityGroup
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Guid> MemberIds { get; set; } = new();
}
