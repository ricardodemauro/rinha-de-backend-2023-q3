using FluentValidation;
using RinhaBackend.NetSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RinhaBackend.NetSolution.Validators;

public sealed partial class PessoaValidator : AbstractValidator<Pessoa>
{
    public PessoaValidator()
    {
        RuleFor(x => x.Apelido).NotEmpty().MaximumLength(32);
        RuleFor(x => x.Nome).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Nascimento).NotNull();
        //RuleFor(x => x.Stack).Must(x => x is null || x.)
    }
}