using FluentAssertions;
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
    public void ShouldReturnError_WhenTopPIs(float topP)
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
    
    [Fact]
    public void ShouldSetSeed_WhenBuildWithSeed()
    {
        ChatCompletion.Builder()
            .WithUserMessage("User message")
            .WithSeed(1337)
            .Build()
            .Should()
            .BeRightWithLog(c => c.Seed.Should().Be(1337));
    }    
    
    [Fact]
    public void ShouldSetTopP_WhenBuildWithTopP()
    {
        ChatCompletion.Builder()
            .WithUserMessage("User message")
            .WithTopP(0.5f)
            .Build()
            .Should()
            .BeRightWithLog(c => c.TopP.Should().Be(0.5f));
    }    
    
    [Fact]
    public void ShouldSetTemperature_WhenBuildWithTemperature()
    {
        ChatCompletion.Builder()
            .WithUserMessage("User message")
            .WithTemperature(0.3f)
            .Build()
            .Should()
            .BeRightWithLog(c => c.Temperature.Should().Be(0.3f));
    } 
    
    [Fact]
    public void ShouldSetStream_WhenBuildWithStream()
    {
        ChatCompletion.Builder()
            .WithUserMessage("User message")
            .WithStream(true)
            .Build()
            .Should()
            .BeRightWithLog(c => c.IsStreamEnabled.Should().BeTrue());
    } 
}