//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//
//namespace Frontend.Data;
//
//public class SpotifyDbContextFactory : IDesignTimeDbContextFactory<SpotifyDbContext>
//{
//    public SpotifyDbContext CreateDbContext(string[] args)
//    {
//        var options = new DbContextOptionsBuilder<SpotifyDbContext>()
//            .UseSqlite("Data Source=design-time.db")
//            .Options;
//
//        return new SpotifyDbContext(options);
//    }
//}