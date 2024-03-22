using System.Text.Json.Serialization;

namespace MistralDotNetClient.Infrastructure.ChatCompletions.Response;

public class MistralChatCompletionResponseMessage {
    [JsonPropertyName("role")] public string Role { get; set; }
    [JsonPropertyName("content")] public string Content { get; set; }
}