using Microsoft.EntityFrameworkCore;
using RinhaBackend.NetSolution.DbContexts;
using RinhaBackend.NetSolution.Routes;
using RinhaBackend.NetSolution.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(o =>
{
    var connectionString = builder.Configuration.GetConnectionString("RinhaDb");
    o.UseNpgsql(connectionString);
});

builder.Services.AddValidators();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.AddRoutePessoas();

#if DEBUG
using var sp = app.Services.CreateScope();
using var db = sp.ServiceProvider.GetRequiredService<AppDbContext>();

await db.Database.EnsureDeletedAsync();
await db.Database.EnsureCreatedAsync();
#endif

app.Run();
