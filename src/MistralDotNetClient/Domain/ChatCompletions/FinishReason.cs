namespace MistralDotNetClient.Domain.ChatCompletions;

public enum FinishReasonType
{
    Stop = 0,
    Length = 1,
    ModelLength = 2,
    Error = 3,
    ToolCalls = 4
}

public class FinishReason
{
    public FinishReasonType Type { get; }

    private FinishReason(FinishReasonType type)
    {
        Type = type;
    }

    public static FinishReason From(string reason)
    {
        var type = Enum.Parse<FinishReasonType>(reason, true);
        return new FinishReason(type);
    }
}