namespace MistralDotNetClient.Common;

public record InternalError(ErrorReason Reason, string Message);

public enum ErrorReason
{
    InvalidParsing,
    InvalidObjectCreation,
    HttpError,
}