using MistralDotNetClient.Domain;
using MistralDotNetClient.Domain.ChatCompletions;

namespace MistralDotNetClient.Common;

public record InternalError(ErrorReason Reason, string Message);

public enum ErrorReason
{
    InvalidParsing,
    InvalidObjectCreation,
    HttpError,
    MaxTokenExceed,
    InvalidModel,
}


public record ChatCompletionMustContainMessages() : InternalError(ErrorReason.InvalidObjectCreation, $"{nameof(ChatCompletion)} must contains at least one message");
public record LastChatCompletionMessageShouldBeUserMessage() : InternalError(ErrorReason.InvalidObjectCreation, $"Last message of type {nameof(ChatCompletion)} must be a user message");
public record MaxTokenCompletionExceeded(int MaxToken) : InternalError(ErrorReason.MaxTokenExceed, "");
public record WrongChatCompletionModel(ModelType ModelType) : InternalError(ErrorReason.InvalidModel, $"Impossible to use model {ModelType.ToString()} for ChatCompletion");