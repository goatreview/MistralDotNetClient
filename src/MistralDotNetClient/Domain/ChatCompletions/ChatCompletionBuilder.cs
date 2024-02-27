using LanguageExt;
using MistralDotNetClient.Common;
using MistralDotNetClient.Domain.ChatCompletions.Messages;

namespace MistralDotNetClient.Domain.ChatCompletions;

public class ChatCompletionBuilder
{
    private readonly List<BaseChatMessage> _messages = [];
    private Model _model = Model.Tiny;

    public ChatCompletionBuilder WithSystemMessage(string message)
    {
        _messages.Add(new SystemChatMessage(message));
        return this;
    }
    
    public ChatCompletionBuilder WithUserMessage(string message)
    {
        _messages.Add(new UserChatMessage(message));
        return this;
    }
    
    public ChatCompletionBuilder WithModel(Model model)
    {
        _model = model;
        return this;
    }
    
    public Either<InternalError, ChatCompletion> Create()
    {
        if(_messages.Last().Type != ChatMessageType.User)
            return new InternalError(ErrorReason.InvalidObjectCreation, $"Last message of type {nameof(ChatCompletion)} must be a system message");
        return new ChatCompletion(_model, _messages);
    }
}