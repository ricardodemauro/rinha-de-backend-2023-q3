using FluentValidation;
using RinhaBackend.NetSolution.Models;

namespace RinhaBackend.NetSolution.Validators;

public static class DependencyExtensions
{
    public static IServiceCollection AddValidators(
        this IServiceCollection services)
    {
        services.AddScoped<IValidator<Pessoa>, PessoaValidator>();

        return services;
    }
}
