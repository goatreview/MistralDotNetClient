using System.Text.Json.Serialization;

namespace MistralDotNetClient.Infrastructure.ChatCompletions.Response;

public class MistralChatCompletionResponseChoice {
    [JsonPropertyName("index")] public int Index { get; set; }
    [JsonPropertyName("message")] public MistralChatCompletionResponseMessage Message { get; set; }
    [JsonPropertyName("finish_reason")] public string FinishReason { get; set; }
}