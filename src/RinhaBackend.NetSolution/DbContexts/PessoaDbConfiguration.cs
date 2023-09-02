using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RinhaBackend.NetSolution.Models;

namespace RinhaBackend.NetSolution.DbContexts;

public class PessoaDbConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.HasIndex(x => x.Apelido).IsUnique();
        builder.HasIndex(x => x.Busca);
    }
}
