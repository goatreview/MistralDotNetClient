namespace MistralDotNetClient.Domain.ChatCompletions;

public class ChatCompletionResponse
{
    public readonly string Message;
    public int TokenUsed { get; }

    private readonly FinishReason _finishReason;
    
    public ChatCompletionResponse(string message,
        FinishReason finishReason,
        int tokenUsed)
    {
        Message = message;
        _finishReason = finishReason;
        TokenUsed = tokenUsed;
    }

    public bool IsSuccessfulResponse()
    {
        return _finishReason.Type == FinishReasonType.Stop;
    }
}