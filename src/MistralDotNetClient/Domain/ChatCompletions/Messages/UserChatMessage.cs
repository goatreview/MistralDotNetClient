namespace MistralDotNetClient.Domain.ChatCompletions.Messages;

public record UserChatMessage(string Content) : BaseChatMessage(ChatMessageType.User, Content);
