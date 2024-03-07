using System.Text.Json.Serialization;

namespace MistralDotNetClient.Infrastructure.Models;

public class MistralModelResponse : IMistralResponse
{
    [JsonPropertyName("object")] public string Object { get; set; }
    [JsonPropertyName("data")] public MistralModel[] Data { get; set; }
}
