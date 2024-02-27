using MistralDotNetClient.Domain.ChatCompletions.Messages;

namespace MistralDotNetClient.Domain.ChatCompletions;

public class ChatCompletion
{
    public Model Model { get; }
    public List<BaseChatMessage> Messages { get; }
    
    internal ChatCompletion(Model model, List<BaseChatMessage> messages)
    {
        Model = model;
        Messages = messages;
    }

    public static ChatCompletionBuilder Build()
    {
        return new ChatCompletionBuilder();
    }
}