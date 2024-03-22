using MistralDotNetClient.Domain;

namespace MistralDotNetClient.Common;

public class MistralClientException(InternalError error) : Exception(error.Message)
{
    public InternalError InternalError { get; } = error;
}