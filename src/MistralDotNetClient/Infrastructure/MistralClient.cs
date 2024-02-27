using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using LanguageExt;
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

    public Either<InternalError, ModelResponse> GetModels()
    {
        return BuildModelHttpRequest()
            .Map(c => SendRequest(c).Result)
            .Map(ExtractResponseData)
            .Bind(ParseResponse<ModelResponse>);
    }
    
    public Either<InternalError, ChatCompletionResponse> CreateChatCompletion(Either<InternalError, ChatCompletion> chatCompletion)
    {
        return chatCompletion
            .Map(c => c.ToRequest())
            .Map(HttpRequestConversion)
            .Map(c => SendRequest(c).Result)
            .Map(ExtractResponseData)
            .Bind(ParseResponse<ChatCompletionResponse>);
    }
    
    public Either<InternalError, EmbeddingResponse> CreateEmbedding(Either<InternalError, Embedding> embedding)
    {
        return embedding
            .Map(c => c.ToRequest())
            .Map(HttpRequestConversion)
            .Map(c => SendRequest(c).Result)
            .Map(ExtractResponseData)
            .Bind(ParseResponse<EmbeddingResponse>);

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
    
    private HttpRequestMessage HttpRequestConversion(ChatCompletionRequest chatRequest)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(_baseUri, "chat/completions"));
        var content = new StringContent(JsonSerializer.Serialize(chatRequest), Encoding.UTF8, "application/json");
        requestMessage.Content = content;
        return requestMessage;
    }
    
    private HttpRequestMessage HttpRequestConversion(EmbeddingRequest embeddingRequest)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(_baseUri, "embeddings"));
        var content = new StringContent(JsonSerializer.Serialize(embeddingRequest), Encoding.UTF8, "application/json");
        requestMessage.Content = content;
        return requestMessage;
    }
    
    private static Either<InternalError, T> ParseResponse<T>(ResponseData response) where T : IResponse
    {
        if (response.IsSuccessStatusCode)
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

        try
        {
            var errorResponse = JsonSerializer.Deserialize<ApiResponseError>(response.Content);
            return errorResponse is null 
                ? new InternalError(ErrorReason.InvalidParsing, $"Impossible to parse {response.Content} to {nameof(ApiResponseError)}") 
                : new InternalError(ErrorReason.HttpError, $"Code: {errorResponse.Code}, Message: {errorResponse.Message}");
        } 
        catch (Exception)
        {
            return new InternalError(ErrorReason.InvalidParsing, $"Impossible to parse {response.Content} to {nameof(ApiResponseError)}");
        }
    }

    private static ResponseData ExtractResponseData(HttpResponseMessage response)
    {
        return new ResponseData(response.StatusCode, response.IsSuccessStatusCode, response.Content.ReadAsStringAsync().Result);
    }
}
