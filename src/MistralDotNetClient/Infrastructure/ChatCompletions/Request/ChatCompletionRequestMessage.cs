using System.Text.Json.Serialization;

namespace MistralDotNetClient.Infrastructure.ChatCompletions.Request;

public class ChatCompletionRequestMessage
{
    [JsonPropertyName("role")] public string Role { get; set; }
    [JsonPropertyName("content")] public string Content { get; set; }
}