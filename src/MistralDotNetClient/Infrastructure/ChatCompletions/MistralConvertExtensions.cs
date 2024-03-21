using MistralDotNetClient.Domain.ChatCompletions;
using MistralDotNetClient.Domain.Embeddings;
using MistralDotNetClient.Infrastructure.ChatCompletions.Request;
using MistralDotNetClient.Infrastructure.ChatCompletions.Response;
using MistralDotNetClient.Infrastructure.Embeddings.Request;

namespace MistralDotNetClient.Infrastructure.ChatCompletions;

public static class MistralConvertExtensions
{
    public static MistralChatCompletionRequest ToRequest(this ChatCompletion chatCompletion)
    {
        var messages = chatCompletion.Messages.Select(message => 
            new MistralChatCompletionRequestMessage { Role = message.Type.ToString().ToLower(), Content = message.Content }).ToArray();
        
        return new MistralChatCompletionRequest
        {
            Model = chatCompletion.Model.Name,
            Messages = messages,
            TopP = 1,
            Stream = false,
            MaxTokens = chatCompletion.MaxTokens,
            Temperature = chatCompletion.Temperature
        };
    }
    
    public static ChatCompletionResponse ToResponse(this MistralChatCompletionResponse mistralResponse)
    {
        var lastCompletion = mistralResponse.Choices.Last();
        return new ChatCompletionResponse(lastCompletion.Message.Content, FinishReason.From(lastCompletion.FinishReason), mistralResponse.Usage.CompletionTokens);
    }
    
    public static MistralEmbeddingRequest ToRequest(this Embedding embedding)
    {
        return new MistralEmbeddingRequest()
        {
            Model = embedding.Model.Name,
            Input = embedding.Inputs.ToArray(),
            Encoding_format = "float"
        };
    }
}