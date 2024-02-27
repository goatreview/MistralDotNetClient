using System.Text.Json.Serialization;

namespace MistralDotNetClient.Infrastructure.Embeddings.Response;

public class EmbeddingResponseData
{
    [JsonPropertyName("object")] public string Object { get; set; }
    [JsonPropertyName("embedding")] public double[] Embedding { get; set; }
    [JsonPropertyName("index")] public int Index { get; set; }
}