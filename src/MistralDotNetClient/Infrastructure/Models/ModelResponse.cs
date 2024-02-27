using System.Text.Json.Serialization;

namespace MistralDotNetClient.Infrastructure.Models;

public class ModelResponse : IResponse
{
    [JsonPropertyName("object")] public string Object { get; set; }
    [JsonPropertyName("data")] public Model[] Data { get; set; }
}
