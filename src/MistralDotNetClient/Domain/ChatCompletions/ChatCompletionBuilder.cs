using LanguageExt;
using MistralDotNetClient.Common;
using MistralDotNetClient.Domain.ChatCompletions.Messages;

namespace MistralDotNetClient.Domain.ChatCompletions;

public class ChatCompletionBuilder
{
    public readonly List<BaseChatMessage> Messages = new();
    public Model Model = Model.Tiny;
    public int? MaxTokens;
    public float Temperature = 0.7f;
    public float TopP = 1f;

    public Either<InternalError, ChatCompletion> Build()
    {
        if(Messages.Count == 0)
            return new ChatCompletionMustContainMessages();
        if(Messages.Last().Type != ChatMessageType.User)
            return new LastChatCompletionMessageShouldBeUserMessage();
        if(Model == Model.Embed)
            return new WrongChatCompletionModel(Model.Type);
        if(MaxTokens < 0)
            return new MaxTokenInvalid();
        if(Temperature is < 0 or > 1)
            return new TemperatureInvalid();
        if(TopP is < 0 or > 1)
            return new TemperatureInvalid();
        return ChatCompletion.FromBuilder(this);
    }
    
    public ChatCompletionBuilder WithSystemMessage(string message)
    {
        Messages.Add(new SystemChatMessage(message));
        return this;
    }
    
    public ChatCompletionBuilder WithUserMessage(string message)
    {
        Messages.Add(new UserChatMessage(message));
        return this;
    }
    
    public ChatCompletionBuilder WithModel(Model model)
    {
        Model = model;
        return this;
    }
    
    public ChatCompletionBuilder WithMaxToken(int maxToken)
    {
        MaxTokens = maxToken;
        return this;
    }

    public ChatCompletionBuilder WithTemperature(float temperature)
    {
        Temperature = temperature;
        return this;
    }

    public ChatCompletionBuilder WithTopP(float topP)
    {
        TopP = topP;
        return this;
    }
}