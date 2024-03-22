using MistralDotNetClient.Domain.ChatCompletions;

namespace MistralDotNetClient.Domain;

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
public record MaxTokenInvalid() : InternalError(ErrorReason.InvalidModel, "MaxTokens must be greater than 0");
public record WrongChatCompletionModel(ModelType ModelType) : InternalError(ErrorReason.InvalidModel, $"Impossible to use model {ModelType.ToString()} for ChatCompletion");
public record TemperatureInvalid() : InternalError(ErrorReason.InvalidModel, "Temperature must be between 0 and 1 included");
public record TopPInvalid() : InternalError(ErrorReason.InvalidModel, "TopP must be between 0 and 1 included");