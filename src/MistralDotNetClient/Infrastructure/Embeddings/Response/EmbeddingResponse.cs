using System.Text.Json.Serialization;

namespace MistralDotNetClient.Infrastructure.Embeddings.Response;

public class EmbeddingResponse : IResponse
{
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("object")] public string Object { get; set; }
    [JsonPropertyName("data")] public EmbeddingResponseData[] Data { get; set; }
    [JsonPropertyName("model")] public string Model { get; set; }
    [JsonPropertyName("usage")] public EmbeddingResponseUsage Usage { get; set; }
}
