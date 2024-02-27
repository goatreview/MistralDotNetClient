using LanguageExt;
using MistralDotNetClient.Common;
using MistralDotNetClient.Domain.ChatCompletions;
using MistralDotNetClient.Domain.Embeddings;
using MistralDotNetClient.Infrastructure.ChatCompletions.Request;
using MistralDotNetClient.Infrastructure.Embeddings.Request;

namespace MistralDotNetClient.Infrastructure.ChatCompletions;

public static class ConvertExtensions
{
    public static ChatCompletionRequest ToRequest(this ChatCompletion chatCompletion)
    {
        var messages = chatCompletion.Messages.Select(message => 
            new ChatCompletionRequestMessage { Role = message.Type.ToString().ToLower(), Content = message.Content }).ToArray();
        
        return new ChatCompletionRequest
        {
            Model = chatCompletion.Model.Name,
            Messages = messages,
            TopP = 1,
        };
    }
    
    public static EmbeddingRequest ToRequest(this Embedding embedding)
    {
        return new EmbeddingRequest()
        {
            Model = embedding.Model.Name,
            Input = embedding.Inputs.ToArray(),
            Encoding_format = "float"
        };
    }
}