using FluentAssertions;
using MistralDotNetClient.Domain.ChatCompletions;
using MistralDotNetClient.Domain.Embeddings;
using MistralDotNetClient.Infrastructure;
using MistralDotNetClient.UnitTests.Common;

namespace MistralDotNetClient.UnitTests;

public class MistralClientTests
{
    private const string API_KEY = "";

    [Fact]
    public void ShouldRetrieveModels()
    {
        MistralClient.Build(API_KEY)
            .GetModels()
            .Should().BeRightWithLog(c => c.Data.Should().HaveCountGreaterThan(0));
    }
    
    [Fact]
    public void ShouldCreateEmbeddings()
    {
        var embedding = Embedding.Build().WithInputs(["Hello", "World"]).Create();
        
        MistralClient.Build(API_KEY)
            .CreateEmbedding(embedding)
            .Should().BeRightWithLog(c => c.Data.Should().HaveCountGreaterThan(0));
    }
}



