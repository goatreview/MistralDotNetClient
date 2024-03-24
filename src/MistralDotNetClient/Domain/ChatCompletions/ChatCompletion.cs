using LanguageExt;
using MistralDotNetClient.Domain.ChatCompletions.Messages;

namespace MistralDotNetClient.Domain.ChatCompletions;

public class ChatCompletion
{
    public Model Model { get; }
    public List<BaseChatMessage> Messages { get; }
    public int? MaxTokens { get; }
    public float Temperature { get; }
    public float TopP { get; }
    public int? Seed { get; }
    public bool IsStreamEnabled { get; }

    private ChatCompletion(
        Model model, 
        List<BaseChatMessage> messages, 
        int? maxTokens, 
        float temperature, 
        float topP,
        int? seed,
        bool isStreamEnabled = false)
    {
        Model = model;
        Messages = messages;
        MaxTokens = maxTokens;
        Temperature = temperature;
        TopP = topP;
        Seed = seed;
        IsStreamEnabled = isStreamEnabled;
    }
    
    public static ChatCompletionBuilder Builder()
    {
        return new ChatCompletionBuilder();
    }

    public static Either<InternalError, ChatCompletion> FromBuilder(ChatCompletionBuilder builder)
    {
        return new ChatCompletion(
            builder.Model, 
            builder.Messages, 
            builder.MaxTokens, 
            builder.Temperature, 
            builder.TopP, 
            builder.Seed, 
            builder.IsStreamEnabled);
    }
}