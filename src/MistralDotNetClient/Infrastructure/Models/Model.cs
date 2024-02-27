using System.Text.Json.Serialization;

namespace MistralDotNetClient.Infrastructure.Models;

public class Model
{
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("object")] public string Object { get; set; }
    [JsonPropertyName("created")] public int Created { get; set; }
    [JsonPropertyName("owned_by")] public string OwnedBy { get; set; }
    [JsonPropertyName("root")] public object Root { get; set; }
    [JsonPropertyName("parent")] public object Parent { get; set; }
    [JsonPropertyName("permission")] public ModelPermission[] Permissions { get; set; }
}