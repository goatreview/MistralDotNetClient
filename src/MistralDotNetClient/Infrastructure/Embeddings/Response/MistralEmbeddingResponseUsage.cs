using System.Text.Json.Serialization;

namespace MistralDotNetClient.Infrastructure.Embeddings.Response;

public class MistralEmbeddingResponseUsage
{
    [JsonPropertyName("prompt_tokens")] public int PromptTokens { get; set; }
    [JsonPropertyName("total_tokens")] public int TotalTokens { get; set; }
}
