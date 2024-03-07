using FluentAssertions;
using MistralDotNetClient.Common;
using MistralDotNetClient.Domain.ChatCompletions;
using MistralDotNetClient.Infrastructure;
using MistralDotNetClient.UnitTests.Common;

namespace MistralDotNetClient.UnitTests;

public class ChatCompletionTests
{
    private const string API_KEY = "";
    
    [Fact]
    public void ShouldCreateChatCompletions()
    {
        var chat = ChatCompletion.Builder().WithUserMessage("What is the current date ?").Build();
        
        MistralClient.Build(API_KEY)
            .CreateChatCompletion(chat)
            .Should()
            .BeRightWithLog(c =>
            {
                c.Message.Should().NotBeNullOrWhiteSpace();
            });
    }
    
    [Fact]
    public void ShouldReturnError_WhenResponseIsBiggerThanMaxToken()
    {
        var maxToken = 1;
        var chat = ChatCompletion.Builder()
            .WithMaxToken(maxToken)
            .WithUserMessage("Respond me with 5 random characters").Build();
        
        MistralClient.Build(API_KEY)
            .CreateChatCompletion(chat)
            .Should()
            .BeLeftWithLog(c =>
            {
                c.Should().BeOfType<MaxTokenCompletionExceeded>();
                ((MaxTokenCompletionExceeded)c).MaxToken.Should().Be(maxToken);
            });
    }
}