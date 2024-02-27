namespace MistralDotNetClient.Common;

public class Monad
{
    // public async Task<Either<TA, TB>> BindAsync<TA, TB>(Func<T, Task<Result<TA, TB>>> bind)
    // {
    //     try
    //     {
    //         return this.IsFailure
    //             ? Result<TB>.FromFailure(this.failure)
    //             : await bind(this.success);
    //     }
    //     catch (Exception exception)
    //     {
    //         return SystemFailure.FromException(exception).ToResult<TB>();
    //     }
    // }
}