using Microsoft.EntityFrameworkCore;
using Frontend.Data.Models;

namespace Frontend.Data;

public class SpotifyDbContext : DbContext
{
    public SpotifyDbContext(DbContextOptions<SpotifyDbContext> options) : base(options) { }

    public DbSet<Marquee> Marquee { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<AlbumImage> AlbumImages { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<PodcastShow> PodcastShows { get; set; }
    public DbSet<StreamingHistoryMusic> StreamingHistoryMusic { get; set; }
    public DbSet<StreamingHistoryPodcast> StreamingHistoryPodcast { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Marquee>(entity =>
        {
            entity.ToTable("marquee");
            entity.HasKey(e => e.ArtistName);
            entity.Property(e => e.ArtistName).HasColumnName("artistName").IsRequired();
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Album>(entity =>
        {
            entity.ToTable("albums");
            entity.HasKey(e => e.AlbumUri);
            entity.Property(e => e.AlbumUri).HasColumnName("albumUri").IsRequired();
            entity.Property(e => e.AlbumName).HasColumnName("albumName").IsRequired();
            entity.Property(e => e.ArtistName).HasColumnName("artistName");
            entity.Property(e => e.AlbumCoverUri).HasColumnName("albumCoverUri");

            entity.HasOne(e => e.Artist)
                  .WithMany(m => m.Albums)
                  .HasForeignKey(e => e.ArtistName);
        });

        modelBuilder.Entity<AlbumImage>(entity =>
        {
            entity.ToTable("albumImage");
            entity.HasKey(e => e.AlbumUri);
            entity.Property(e => e.AlbumUri).HasColumnName("albumUri").IsRequired();
            entity.Property(e => e.ImageBlob).HasColumnName("imageBlob").HasColumnType("BLOB");

            entity.HasOne(e => e.Album)
                  .WithOne(a => a.AlbumImage)
                  .HasForeignKey<AlbumImage>(e => e.AlbumUri);
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.ToTable("tracks");
            entity.HasKey(e => e.TrackUri);
            entity.Property(e => e.TrackUri).HasColumnName("trackUri").IsRequired();
            entity.Property(e => e.TrackName).HasColumnName("trackName").IsRequired();
            entity.Property(e => e.ArtistName).HasColumnName("artistName");
            entity.Property(e => e.AlbumUri).HasColumnName("albumUri");
            entity.Property(e => e.Skipped).HasColumnName("skipped").HasDefaultValue(0);
            entity.Property(e => e.Shuffled).HasColumnName("shuffled").HasDefaultValue(0);
            entity.Property(e => e.MsPlayed).HasColumnName("msPlayed").HasDefaultValue(0L);

            entity.HasOne(e => e.Artist)
                  .WithMany(m => m.Tracks)
                  .HasForeignKey(e => e.ArtistName);

            entity.HasOne(e => e.Album)
                  .WithMany(a => a.Tracks)
                  .HasForeignKey(e => e.AlbumUri);
        });

        modelBuilder.Entity<PodcastShow>(entity =>
        {
            entity.ToTable("podcastShows");
            entity.HasKey(e => e.ShowName);
            entity.Property(e => e.ShowName).HasColumnName("showName").IsRequired();
            entity.Property(e => e.MsPlayed).HasColumnName("msPlayed").HasDefaultValue(0L);
        });

        modelBuilder.Entity<StreamingHistoryMusic>(entity =>
        {
            entity.ToTable("streamingHistoryMusic");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.Ts).HasColumnName("ts").IsRequired();
            entity.Property(e => e.MsPlayed).HasColumnName("msPlayed").HasDefaultValue(0L);
            entity.Property(e => e.TrackUri).HasColumnName("trackUri");
            entity.Property(e => e.TrackName).HasColumnName("trackName");
            entity.Property(e => e.ArtistName).HasColumnName("artistName");
            entity.Property(e => e.AlbumName).HasColumnName("albumName");
            entity.Property(e => e.Shuffled).HasColumnName("shuffled").HasDefaultValue(0);
            entity.Property(e => e.Skipped).HasColumnName("skipped").HasDefaultValue(0);
            entity.Property(e => e.Offline).HasColumnName("offline").HasDefaultValue(0);
            entity.Property(e => e.ReasonEnd).HasColumnName("reasonEnd");
            entity.Property(e => e.ReasonStart).HasColumnName("reasonStart");

            entity.HasOne(e => e.Track)
                  .WithMany(t => t.StreamingHistory)
                  .HasForeignKey(e => e.TrackUri);

            entity.HasOne(e => e.Artist)
                  .WithMany(m => m.StreamingHistory)
                  .HasForeignKey(e => e.ArtistName);

            entity.HasIndex(e => e.Ts).HasDatabaseName("idxMusicHistoryTs");
            entity.HasIndex(e => e.TrackUri).HasDatabaseName("idxMusicHistoryTrack");
            entity.HasIndex(e => e.ArtistName).HasDatabaseName("idxMusicHistoryArtist");
        });

        modelBuilder.Entity<StreamingHistoryPodcast>(entity =>
        {
            entity.ToTable("streamingHistoryPodcast");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.Ts).HasColumnName("ts").IsRequired();
            entity.Property(e => e.MsPlayed).HasColumnName("msPlayed").HasDefaultValue(0L);
            entity.Property(e => e.EpisodeName).HasColumnName("episodeName");
            entity.Property(e => e.ShowName).HasColumnName("showName");
            entity.Property(e => e.EpisodeUri).HasColumnName("episodeUri");

            entity.HasOne(e => e.Show)
                  .WithMany(s => s.StreamingHistory)
                  .HasForeignKey(e => e.ShowName);

            entity.HasIndex(e => e.Ts).HasDatabaseName("idxPodcastHistoryTs");
            entity.HasIndex(e => e.ShowName).HasDatabaseName("idxPodcastHistoryShow");
        });
    }
}