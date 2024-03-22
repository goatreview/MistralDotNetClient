using LanguageExt;
using MistralDotNetClient.Domain;

namespace MistralDotNetClient.Common;

public static class EitherExtensions
{
    public static TResult GetUnsafe<TResult>(this Either<InternalError, TResult> either)
    {
        return either.Match(
            right => right,
            error => throw new MistralClientException(error)
        );
    }
}

