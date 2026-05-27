using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Frontend.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "marquee",
                columns: table => new
                {
                    artistName = table.Column<string>(type: "TEXT", nullable: false),
                    status = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marquee", x => x.artistName);
                });

            migrationBuilder.CreateTable(
                name: "podcastShows",
                columns: table => new
                {
                    showName = table.Column<string>(type: "TEXT", nullable: false),
                    msPlayed = table.Column<long>(type: "INTEGER", nullable: false, defaultValue: 0L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_podcastShows", x => x.showName);
                });

            migrationBuilder.CreateTable(
                name: "albums",
                columns: table => new
                {
                    albumUri = table.Column<string>(type: "TEXT", nullable: false),
                    albumName = table.Column<string>(type: "TEXT", nullable: false),
                    artistName = table.Column<string>(type: "TEXT", nullable: true),
                    albumCoverUri = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_albums", x => x.albumUri);
                    table.ForeignKey(
                        name: "FK_albums_marquee_artistName",
                        column: x => x.artistName,
                        principalTable: "marquee",
                        principalColumn: "artistName");
                });

            migrationBuilder.CreateTable(
                name: "streamingHistoryPodcast",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ts = table.Column<long>(type: "INTEGER", nullable: false),
                    msPlayed = table.Column<long>(type: "INTEGER", nullable: false, defaultValue: 0L),
                    episodeName = table.Column<string>(type: "TEXT", nullable: true),
                    showName = table.Column<string>(type: "TEXT", nullable: true),
                    episodeUri = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_streamingHistoryPodcast", x => x.id);
                    table.ForeignKey(
                        name: "FK_streamingHistoryPodcast_podcastShows_showName",
                        column: x => x.showName,
                        principalTable: "podcastShows",
                        principalColumn: "showName");
                });

            migrationBuilder.CreateTable(
                name: "albumImage",
                columns: table => new
                {
                    albumUri = table.Column<string>(type: "TEXT", nullable: false),
                    imageBlob = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_albumImage", x => x.albumUri);
                    table.ForeignKey(
                        name: "FK_albumImage_albums_albumUri",
                        column: x => x.albumUri,
                        principalTable: "albums",
                        principalColumn: "albumUri",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tracks",
                columns: table => new
                {
                    trackUri = table.Column<string>(type: "TEXT", nullable: false),
                    trackName = table.Column<string>(type: "TEXT", nullable: false),
                    artistName = table.Column<string>(type: "TEXT", nullable: true),
                    albumUri = table.Column<string>(type: "TEXT", nullable: true),
                    skipped = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    shuffled = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    msPlayed = table.Column<long>(type: "INTEGER", nullable: false, defaultValue: 0L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tracks", x => x.trackUri);
                    table.ForeignKey(
                        name: "FK_tracks_albums_albumUri",
                        column: x => x.albumUri,
                        principalTable: "albums",
                        principalColumn: "albumUri");
                    table.ForeignKey(
                        name: "FK_tracks_marquee_artistName",
                        column: x => x.artistName,
                        principalTable: "marquee",
                        principalColumn: "artistName");
                });

            migrationBuilder.CreateTable(
                name: "streamingHistoryMusic",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ts = table.Column<long>(type: "INTEGER", nullable: false),
                    msPlayed = table.Column<long>(type: "INTEGER", nullable: false, defaultValue: 0L),
                    trackUri = table.Column<string>(type: "TEXT", nullable: true),
                    trackName = table.Column<string>(type: "TEXT", nullable: true),
                    artistName = table.Column<string>(type: "TEXT", nullable: true),
                    albumName = table.Column<string>(type: "TEXT", nullable: true),
                    shuffled = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    skipped = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    offline = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    reasonEnd = table.Column<string>(type: "TEXT", nullable: true),
                    reasonStart = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_streamingHistoryMusic", x => x.id);
                    table.ForeignKey(
                        name: "FK_streamingHistoryMusic_marquee_artistName",
                        column: x => x.artistName,
                        principalTable: "marquee",
                        principalColumn: "artistName");
                    table.ForeignKey(
                        name: "FK_streamingHistoryMusic_tracks_trackUri",
                        column: x => x.trackUri,
                        principalTable: "tracks",
                        principalColumn: "trackUri");
                });

            migrationBuilder.CreateIndex(
                name: "IX_albums_artistName",
                table: "albums",
                column: "artistName");

            migrationBuilder.CreateIndex(
                name: "idxMusicHistoryArtist",
                table: "streamingHistoryMusic",
                column: "artistName");

            migrationBuilder.CreateIndex(
                name: "idxMusicHistoryTrack",
                table: "streamingHistoryMusic",
                column: "trackUri");

            migrationBuilder.CreateIndex(
                name: "idxMusicHistoryTs",
                table: "streamingHistoryMusic",
                column: "ts");

            migrationBuilder.CreateIndex(
                name: "idxPodcastHistoryShow",
                table: "streamingHistoryPodcast",
                column: "showName");

            migrationBuilder.CreateIndex(
                name: "idxPodcastHistoryTs",
                table: "streamingHistoryPodcast",
                column: "ts");

            migrationBuilder.CreateIndex(
                name: "IX_tracks_albumUri",
                table: "tracks",
                column: "albumUri");

            migrationBuilder.CreateIndex(
                name: "IX_tracks_artistName",
                table: "tracks",
                column: "artistName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "albumImage");

            migrationBuilder.DropTable(
                name: "streamingHistoryMusic");

            migrationBuilder.DropTable(
                name: "streamingHistoryPodcast");

            migrationBuilder.DropTable(
                name: "tracks");

            migrationBuilder.DropTable(
                name: "podcastShows");

            migrationBuilder.DropTable(
                name: "albums");

            migrationBuilder.DropTable(
                name: "marquee");
        }
    }
}
