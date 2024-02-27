namespace MistralDotNetClient.Domain.ChatCompletions.Messages;

public abstract record BaseChatMessage(ChatMessageType Type, string Content);
