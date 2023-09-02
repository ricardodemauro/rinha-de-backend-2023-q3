using System.Text.Json;

namespace RinhaBackend.NetSolution.Test;

public class UnitTest1
{
    [Fact]
    public void SerializeTest()
    {
        DateOnly d = new DateOnly(2023, 01, 02);

        var json = new { Nascimento = d };

        var jsonOpts = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var rawJson = System.Text.Json.JsonSerializer.Serialize(json, jsonOpts);

        string expectedJson = "{\"nascimento\":\"2023-01-02\"}";
        Assert.Equal(expectedJson, rawJson);
    }
}