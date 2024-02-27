using System.Text.Json.Serialization;

namespace MistralDotNetClient.Infrastructure.ChatCompletions.Request;

public class ChatCompletionRequest
{
    [JsonPropertyName("model")] public string Model { get; set; }
    [JsonPropertyName("messages")] public ChatCompletionRequestMessage[] Messages { get; set; }
    [JsonPropertyName("temperature")] public double Temperature { get; set; }
    [JsonPropertyName("top_p")] public int TopP { get; set; }
    [JsonPropertyName("max_tokens")] public int MaxTokens { get; set; }
    [JsonPropertyName("stream")] public bool Stream { get; set; }
    [JsonPropertyName("safe_prompt")] public bool SafePrompt { get; set; }
    [JsonPropertyName("random_seed")] public object? RandomSeed { get; set; }
}
