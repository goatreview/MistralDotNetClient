using System.Text.Json.Serialization;

namespace MistralDotNetClient.Infrastructure.Embeddings.Response;

public class MistralEmbeddingResponse : IMistralResponse
{
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("object")] public string Object { get; set; }
    [JsonPropertyName("data")] public MistralEmbeddingResponseData[] Data { get; set; }
    [JsonPropertyName("model")] public string Model { get; set; }
    [JsonPropertyName("usage")] public MistralEmbeddingResponseUsage Usage { get; set; }
}
