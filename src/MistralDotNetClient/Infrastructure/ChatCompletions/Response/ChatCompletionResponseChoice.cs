using System.Text.Json.Serialization;

namespace MistralDotNetClient.Infrastructure.ChatCompletions.Response;

public class ChatCompletionResponseChoice {
    [JsonPropertyName("index")] public int Index { get; set; }
    [JsonPropertyName("message")] public ChatCompletionResponseMessage Message { get; set; }
    [JsonPropertyName("finish_reason")] public string FinishReason { get; set; }
}