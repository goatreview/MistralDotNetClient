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
    
    [Theory]
    [InlineData(-0.01)]
    [InlineData(1.01)]
    public void ShouldReturnError_WhenTemperatureIs(float temperature)
    {
        ChatCompletion.Builder()
            .WithUserMessage("User message")
            .WithTemperature(temperature)
            .Build()
            .Should()
            .BeLeftWithLog(c => c.Should().BeOfType<TemperatureInvalid>());
    }
    
    [Theory]
    [InlineData(-0.01)]
    [InlineData(1.01)]
    public void ShouldReturnError_WhenTopPis(float topP)
    {
        ChatCompletion.Builder()
            .WithUserMessage("User message")
            .WithTopP(topP)
            .Build()
            .Should()
            .BeLeftWithLog(c => c.Should().BeOfType<TopPInvalid>());
    }
    
    [Fact]
    public void ShouldReturnError_WhenMaxTokensIsLowerThan0()
    {
        ChatCompletion.Builder()
            .WithUserMessage("User message")
            .WithMaxToken(-1)
            .Build()
            .Should()
            .BeLeftWithLog(c => c.Should().BeOfType<MaxTokenInvalid>());
    }    
}