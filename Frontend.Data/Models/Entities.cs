using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frontend.Data.Models;

public class Marquee
{
    [Key]
    public string ArtistName { get; set; } = string.Empty;
    public string? Status { get; set; }

    public ICollection<Album> Albums { get; set; } = [];
    public ICollection<Track> Tracks { get; set; } = [];
    public ICollection<StreamingHistoryMusic> StreamingHistory { get; set; } = [];
}

public class Album
{
    [Key]
    public string AlbumUri { get; set; } = string.Empty;
    public string AlbumName { get; set; } = string.Empty;
    public string? ArtistName { get; set; }
    public string? AlbumCoverUri { get; set; }

    public Marquee? Artist { get; set; }
    public AlbumImage? AlbumImage { get; set; }
    public ICollection<Track> Tracks { get; set; } = [];
}

public class AlbumImage
{
    [Key]
    public string AlbumUri { get; set; } = string.Empty;
    public byte[]? ImageBlob { get; set; }

    public Album? Album { get; set; }
}

public class Track
{
    [Key]
    public string TrackUri { get; set; } = string.Empty;
    public string TrackName { get; set; } = string.Empty;
    public string? ArtistName { get; set; }
    public string? AlbumUri { get; set; }
    public int Skipped { get; set; } = 0;
    public int Shuffled { get; set; } = 0;
    public long MsPlayed { get; set; } = 0;

    public Marquee? Artist { get; set; }
    public Album? Album { get; set; }
    public ICollection<StreamingHistoryMusic> StreamingHistory { get; set; } = [];
}

public class PodcastShow
{
    [Key]
    public string ShowName { get; set; } = string.Empty;
    public long MsPlayed { get; set; } = 0;

    public ICollection<StreamingHistoryPodcast> StreamingHistory { get; set; } = [];
}

public class StreamingHistoryMusic
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public long Ts { get; set; }
    public long MsPlayed { get; set; } = 0;
    public string? TrackUri { get; set; }
    public string? TrackName { get; set; }
    public string? ArtistName { get; set; }
    public string? AlbumName { get; set; }
    public int Shuffled { get; set; } = 0;
    public int Skipped { get; set; } = 0;
    public int Offline { get; set; } = 0;
    public string? ReasonEnd { get; set; }
    public string? ReasonStart { get; set; }

    public Track? Track { get; set; }
    public Marquee? Artist { get; set; }
}

public class StreamingHistoryPodcast
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public long Ts { get; set; }
    public long MsPlayed { get; set; } = 0;
    public string? EpisodeName { get; set; }
    public string? ShowName { get; set; }
    public string? EpisodeUri { get; set; }

    public PodcastShow? Show { get; set; }
}