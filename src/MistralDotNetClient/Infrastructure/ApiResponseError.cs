using System.Text.Json.Serialization;

namespace MistralDotNetClient.Infrastructure;

public class ApiResponseError : IResponse
{
    [JsonPropertyName("code")] public string Code { get; set; }
    [JsonPropertyName("message")] public string Message { get; set; }
    [JsonPropertyName("object")] public string Object { get; set; }
    [JsonPropertyName("param")] public object Param { get; set; }
    [JsonPropertyName("type")] public string Type { get; set; }
}
