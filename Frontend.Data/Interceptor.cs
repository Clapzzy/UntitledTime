using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
namespace Frontend.Data.Services;

public class SqlitePragmaInterceptor : DbConnectionInterceptor
{
    public override void ConnectionOpened(
        DbConnection connection,
        ConnectionEndEventData eventData)
    {
        using var cmd = connection.CreateCommand();
        cmd.CommandText = "PRAGMA foreign_keys = OFF;";
        cmd.ExecuteNonQuery();
    }
}