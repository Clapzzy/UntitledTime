using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SqliteWasmBlazor;
using Frontend.Data;
using Frontend.Data.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSqliteWasm();
builder.Services.AddDbContextFactory<SpotifyDbContext>(options =>
{
    var connection = new SqliteWasmConnection("Data Source=SpotifyAnalytics.db");
    options.UseSqliteWasm(connection);
    //options.AddInterceptors(new SqlitePragmaInterceptor());
});


var host = builder.Build();

await host.Services.InitializeSqliteWasmDatabaseAsync<SpotifyDbContext>();

await host.RunAsync();