namespace MistralDotNetClient.Domain.ChatCompletions.Messages;

public record SystemChatMessage(string Content) : BaseChatMessage(ChatMessageType.System, Content);
