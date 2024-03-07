using LanguageExt;
using MistralDotNetClient.Common;
using MistralDotNetClient.Domain.ChatCompletions.Messages;
using MistralDotNetClient.Infrastructure.ChatCompletions.Response;

namespace MistralDotNetClient.Domain.ChatCompletions;

public class ChatCompletionResponse
{
    public readonly string Message;
    public int TokenUsed { get; }

    private readonly string _finishReason;
    
    public ChatCompletionResponse(string message,
        string finishReason,
        int tokenUsed)
    {
        Message = message;
        _finishReason = finishReason;
        TokenUsed = tokenUsed;
    }

    public bool IsSuccessfulResponse()
    {
        return _finishReason == "stop";
    }
}