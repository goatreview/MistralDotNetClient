using LanguageExt;
using MistralDotNetClient.Common;
using MistralDotNetClient.Domain.ChatCompletions.Messages;

namespace MistralDotNetClient.Domain.ChatCompletions;

public class ChatCompletion
{
    public Model Model { get; }
    public List<BaseChatMessage> Messages { get; }
    public int? MaxTokens { get; }

    private ChatCompletion(Model model, List<BaseChatMessage> messages, int? maxTokens)
    {
        Model = model;
        Messages = messages;
        MaxTokens = maxTokens;
    }
    
    public static ChatCompletionBuilder Builder()
    {
        return new ChatCompletionBuilder();
    }

    public static Either<InternalError, ChatCompletion> FromBuilder(ChatCompletionBuilder builder)
    {
        return new ChatCompletion(builder.Model, builder.Messages, builder.MaxTokens);
    }
}