using FluentAssertions;
using FluentAssertions.LanguageExt;
using MistralDotNetClient.Domain.ChatCompletions;
using MistralDotNetClient.Domain.Embeddings;
using MistralDotNetClient.Infrastructure;

namespace MistralDotNetClient.UnitTests;

public class MistralClientTests
{
    private const string API_KEY = "";

    [Fact]
    public void ShouldRetrieveModels()
    {
        MistralClient.Build(API_KEY)
            .GetModels()
            .Should().BeRight(c => c.Data.Should().HaveCountGreaterThan(0));
    }

    [Fact]
    public void ShouldCreateChatCompletions()
    {
        var chat = ChatCompletion.Build().WithUserMessage("What is the current date ?").Create();
        
        MistralClient.Build(API_KEY)
            .CreateChatCompletion(chat)
            .Should().BeRight(c => c.Choices.Should().HaveCountGreaterThan(0));
    }
    
    [Fact]
    public void ShouldCreateEmbeddings()
    {
        var embedding = Embedding.Build().WithInputs(["Hello", "World"]).Create();
        
        MistralClient.Build(API_KEY)
            .CreateEmbedding(embedding)
            .Should().BeRight(c => c.Data.Should().HaveCountGreaterThan(0));
    }
}

