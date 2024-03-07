using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using LanguageExt;

namespace MistralDotNetClient.UnitTests.Common;

public static class LanguageExtEitherAssertionsExtensions
{
    public static LanguageExtEitherAssertions<TL, TR> Should<TL, TR>(this Either<TL, TR> instance) => new(instance);
}

public class LanguageExtEitherAssertions<TL, TR>(Either<TL, TR> subject)
    : ReferenceTypeAssertions<Either<TL, TR>, LanguageExtEitherAssertions<TL, TR>>(subject)
{
    protected override string Identifier { get; }
    
    public void BeRightWithLog(Action<TR> action, string because = "", params object[] becauseArgs)
    {
        var errorMessage = Subject.LeftToList().FirstOrDefault()?.ToString() ?? "";
        BeRight(errorMessage, becauseArgs);
        Subject.IfRight(action);
    }

    private void BeRight(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:either} to be Right{reason}, ")
            .Given(() => Subject)
            .ForCondition(subject => subject.IsRight)
            .FailWith("but found to be Left.");
    }
    
    public void BeLeftWithLog(Action<TL> action, string because = "", params object[] becauseArgs)
    {
        var errorMessage = Subject.LeftToList().FirstOrDefault()?.ToString() ?? "";
        BeLeft(errorMessage, becauseArgs);
        Subject.IfLeft(action);
    }
    
    private void BeLeft(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:either} to be Left{reason}, ")
            .Given(() => Subject)
            .ForCondition(subject => subject.IsLeft)
            .FailWith("but found to be Right.");
    }
}