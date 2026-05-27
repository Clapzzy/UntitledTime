PRAGMA foreign_keys = OFF;

CREATE TABLE marquee (
    artistName TEXT PRIMARY KEY NOT NULL,
    status TEXT
);

CREATE TABLE albums (
    albumUri TEXT PRIMARY KEY NOT NULL,
    albumName TEXT NOT NULL,
    artistName TEXT,
    albumCoverUri TEXT,
    FOREIGN KEY (artistName) REFERENCES marquee(artistName) 
);

CREATE TABLE albumImage(
    albumUri TEXT,
    imageBlob BLOB
    FOREIGN KEY (albumUri) REFERENCES albums(albumUri)
);

CREATE TABLE tracks (
    trackUri TEXT PRIMARY KEY NOT NULL,
    trackName TEXT NOT NULL,
    artistName TEXT,
    albumUri TEXT,
    skipped INTEGER DEFAULT 0,
    shuffled INTEGER DEFAULT 0,
    msPlayed INTEGER DEFAULT 0,
    FOREIGN KEY (artistName) REFERENCES marquee(artistName) ,
    FOREIGN KEY (albumUri) REFERENCES albums(albumUri)
);

CREATE TABLE podcastShows (
    showName TEXT PRIMARY KEY NOT NULL,
    msPlayed INTEGER DEFAULT 0
);

CREATE TABLE streamingHistoryMusic (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    ts INTEGER NOT NULL,
    msPlayed INTEGER DEFAULT 0,
    trackUri TEXT,
    trackName TEXT,
    artistName TEXT,
    albumName TEXT,
    shuffled INTEGER DEFAULT 0,  
    skipped INTEGER DEFAULT 0,
    offline INTEGER DEFAULT 0,
    reasonEnd TEXT,
    reasonStart TEXT,
    FOREIGN KEY (trackUri) REFERENCES tracks(trackUri) ,
    FOREIGN KEY (artistName) REFERENCES marquee(artistName) 
);

CREATE TABLE streamingHistoryPodcast (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    ts INTEGER NOT NULL,
    msPlayed INTEGER DEFAULT 0,
    episodeName TEXT,
    showName TEXT,
    episodeUri TEXT,
    FOREIGN KEY (showName) REFERENCES podcastShows(showName) 
);

CREATE INDEX idx_music_history_ts ON streamingHistoryMusic(ts);
CREATE INDEX idx_music_history_track ON streamingHistoryMusic(trackUri);
CREATE INDEX idx_music_history_artist ON streamingHistoryMusic(artistName);
CREATE INDEX idx_podcast_history_ts ON streamingHistoryPodcast(ts);
CREATE INDEX idx_podcast_history_show ON streamingHistoryPodcast(showName);