using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;
using MistralDotNetClient.Common;
using MistralDotNetClient.Domain.ChatCompletions;
using MistralDotNetClient.Domain.Embeddings;
using MistralDotNetClient.Infrastructure.ChatCompletions;
using MistralDotNetClient.Infrastructure.ChatCompletions.Request;
using MistralDotNetClient.Infrastructure.ChatCompletions.Response;
using MistralDotNetClient.Infrastructure.Embeddings.Request;
using MistralDotNetClient.Infrastructure.Embeddings.Response;
using MistralDotNetClient.Infrastructure.Models;

namespace MistralDotNetClient.Infrastructure;

public record ResponseData(HttpStatusCode StatusCode, bool IsSuccessStatusCode, string Content);

public class MistralClient
{
    private readonly Uri _baseUri = new Uri("https://api.mistral.ai/v1/");
    private readonly HttpClient _client;

    private MistralClient(string apiKey)
    {
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
    }

    public static MistralClient Build(string apiKey)
    {
        return new MistralClient(apiKey);
    }

    public Either<InternalError, MistralModelResponse> GetModels()
    {
        return BuildModelHttpRequest()
            .Map(c => SendRequest(c).Result)
            .Map(ExtractResponseData)
            .Do(Console.WriteLine)
            .Bind(r => r.IsSuccessStatusCode ? ParseResponse<MistralModelResponse>(r) : ParseError<MistralModelResponse>(r));
    }
    
    public Either<InternalError, ChatCompletionResponse> CreateChatCompletion(Either<InternalError, ChatCompletion> chatCompletion)
    {
        return chatCompletion
            .Map(c => c.ToRequest())
            .Map(HttpRequestConversion)
            .Map(c => SendRequest(c).Result)
            .Map(ExtractResponseData)
            .Do(Console.WriteLine)
            .Bind(r => r.IsSuccessStatusCode ? ParseResponse<MistralChatCompletionResponse>(r) : ParseError<MistralChatCompletionResponse>(r))
            .Map(c => c.ToResponse())
            .Bind(VerifyFinishReason);
    }
    
    public Either<InternalError, MistralEmbeddingResponse> CreateEmbedding(Either<InternalError, Embedding> embedding)
    {
        return embedding
            .Map(c => c.ToRequest())
            .Map(HttpRequestConversion)
            .Map(c => SendRequest(c).Result)
            .Map(ExtractResponseData)
            .Do(Console.WriteLine)
            .Bind(r => r.IsSuccessStatusCode
                ? ParseResponse<MistralEmbeddingResponse>(r)
                : ParseError<MistralEmbeddingResponse>(r));
    }
    
    private static Either<InternalError, ChatCompletionResponse> VerifyFinishReason(ChatCompletionResponse response)
    {
        if (response.IsSuccessfulResponse())
            return response;
        return new MaxTokenCompletionExceeded(response.TokenUsed);
    }

    private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request)
    {
        return await _client.SendAsync(request);
    }
    
    private Either<InternalError, HttpRequestMessage> BuildModelHttpRequest()
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(_baseUri, "models"));
        return requestMessage;
    }
    
    private HttpRequestMessage HttpRequestConversion(MistralChatCompletionRequest chatRequest)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(_baseUri, "chat/completions"));
        var content = new StringContent(JsonSerializer.Serialize(chatRequest), Encoding.UTF8, "application/json");
        requestMessage.Content = content;
        return requestMessage;
    }
    
    private HttpRequestMessage HttpRequestConversion(MistralEmbeddingRequest embeddingRequest)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(_baseUri, "embeddings"));
        var content = new StringContent(JsonSerializer.Serialize(embeddingRequest), Encoding.UTF8, "application/json");
        requestMessage.Content = content;
        return requestMessage;
    }
    
    private static Either<InternalError, T> ParseResponse<T>(ResponseData response) where T : IMistralResponse
    {
        try
        {
            var successResponse = JsonSerializer.Deserialize<T>(response.Content);
            if (successResponse is null)
                return new InternalError(ErrorReason.InvalidParsing, $"Impossible to parse {response.Content} to {nameof(T)}");
            return successResponse;
        } 
        catch (Exception)
        {
            return new InternalError(ErrorReason.InvalidParsing, $"Impossible to parse {response.Content} to {nameof(T)}");
        }
    }
    
    private static Either<InternalError, T> ParseError<T>(ResponseData response) where T : IMistralResponse
    {
        try
        {
            var errorResponse = JsonSerializer.Deserialize<MistralApiResponseError>(response.Content);
            return errorResponse is null 
                ? new InternalError(ErrorReason.InvalidParsing, $"Impossible to parse {response.Content} to {nameof(MistralApiResponseError)}") 
                : new InternalError(ErrorReason.HttpError, $"Code: {errorResponse.Code}, Message: {errorResponse.Message}");
        } 
        catch (Exception)
        {
            return new InternalError(ErrorReason.InvalidParsing, $"Impossible to parse {response.Content} to {nameof(MistralApiResponseError)}");
        }
    }

    private static ResponseData ExtractResponseData(HttpResponseMessage response)
    {
        return new ResponseData(response.StatusCode, response.IsSuccessStatusCode, response.Content.ReadAsStringAsync().Result);
    }
}
