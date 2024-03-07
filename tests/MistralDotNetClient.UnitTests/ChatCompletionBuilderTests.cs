using FluentAssertions;
using MistralDotNetClient.Common;
using MistralDotNetClient.Domain;
using MistralDotNetClient.Domain.ChatCompletions;
using MistralDotNetClient.UnitTests.Common;

namespace MistralDotNetClient.UnitTests;

public class ChatCompletionBuilderTests
{
    [Fact]
    public void ShouldReturnError_WhenCompletionContainsNoUserMessage()
    {
        ChatCompletion.Builder()
            .Build()
            .Should()
            .BeLeftWithLog(c => c.Should().BeOfType<ChatCompletionMustContainMessages>());
    }
    
    [Fact]
    public void ShouldReturnError_WhenCompletionDoesNotEndWithUserMessage()
    {
        ChatCompletion.Builder()
            .WithUserMessage("User message")
            .WithSystemMessage("System message")
            .Build()
            .Should()
            .BeLeftWithLog(c => c.Should().BeOfType<LastChatCompletionMessageShouldBeUserMessage>());
    }
    
    [Fact]
    public void ShouldReturnError_WhenUsingModelEmbedFromChatCompletion()
    {
        ChatCompletion.Builder()
            .WithUserMessage("Fake message here")
            .WithModel(Model.Embed)
            .Build()
            .Should()
            .BeLeftWithLog(c => c.Should().BeOfType<WrongChatCompletionModel>());
    }
}