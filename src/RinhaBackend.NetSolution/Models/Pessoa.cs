using System.Text.Json.Serialization;

namespace RinhaBackend.NetSolution.Models;

public class Pessoa
{
    public string Id { get; set; }

    public string Apelido { get; set; }

    public string Nome { get; set; }

    public DateOnly? Nascimento { get; set; }

    public string[] Stack { get; set; }

    [JsonIgnore]
    public string Busca { get; set; }
}
