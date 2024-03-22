using System.Text.Json.Serialization;

namespace MistralDotNetClient.Infrastructure.Embeddings.Request;

public class MistralEmbeddingRequest
{
    [JsonPropertyName("model")] public string Model { get; set; }
    [JsonPropertyName("input")] public string[] Input { get; set; }
    [JsonPropertyName("encoding_format")] public string Encoding_format { get; set; }
}


