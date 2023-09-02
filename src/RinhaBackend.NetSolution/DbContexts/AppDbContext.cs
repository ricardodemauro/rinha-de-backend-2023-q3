using Microsoft.EntityFrameworkCore;
using RinhaBackend.NetSolution.Models;

namespace RinhaBackend.NetSolution.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pessoa> Pessoas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PessoaDbConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
