using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RinhaBackend.NetSolution.DbContexts;
using RinhaBackend.NetSolution.Models;
using System.Data.Common;

namespace RinhaBackend.NetSolution.Routes;

public static class PessoasRouteExtensions
{
    public static WebApplication AddRoutePessoas(this WebApplication app)
    {
        app.MapPost("/pessoas", (
            [FromBody] Pessoa data,
            AppDbContext db,
            IValidator<Pessoa> validator) => CriarPessoa(data, db, validator));

        app.MapGet("/pessoas/{id}", (
            [FromRoute] string id,
            AppDbContext db) => GetPessoa(id, db));

        app.MapGet("/pessoas", (
            [FromQuery] string t,
            AppDbContext db) => BuscaPessoas(termo: t, db));

        return app;
    }

    static async Task<Results<BadRequest, Ok<Pessoa[]>>> BuscaPessoas(string termo, AppDbContext db)
    {
        if (string.IsNullOrEmpty(termo)) return TypedResults.BadRequest();

        var pessoas = await db.Pessoas.Where
        (
            x => EF.Functions.Like(x.Nome, termo)
            || EF.Functions.Like(x.Apelido, termo)
            //|| x.Stack.Any(s => EF.Functions.Like(s, termo))
            //|| x.Stack.Any(s => s.Contains(termo))
        )
        .AsNoTracking()
        .ToArrayAsync();

        return TypedResults.Ok(pessoas);
    }

    static async Task<Results<UnprocessableEntity, Created<Pessoa>>> CriarPessoa(
        Pessoa data,
        AppDbContext db,
        IValidator<Pessoa> validator)
    {
        var validationResult = validator.Validate(data);
        if (!validationResult.IsValid)
        {
            return TypedResults.UnprocessableEntity();
        }

        data.Id = Guid.NewGuid().ToString();
        try
        {
            db.Pessoas.Add(data);
            await db.SaveChangesAsync();
        }
        catch (Exception e) when (e is DbUpdateException || e is DbException)
        {
            //IX_Pessoas_Apelido
            return TypedResults.UnprocessableEntity();
        }

        return TypedResults.Created($"pessoas/{data.Id}", data);
    }

    static async Task<Results<NotFound, Ok<Pessoa>>> GetPessoa(
       string id,
       AppDbContext db)
    {
        var pessoa = await db.Pessoas.FindAsync(id);

        if (pessoa is null) return TypedResults.NotFound();

        return TypedResults.Ok(pessoa);
    }
}
