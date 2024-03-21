using FluentAssertions;
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
        var embedding = Embedding.Build().WithInputs(new[] {"Hello", "World"}).Create();

        var results = MistralClient.Build(API_KEY)
            .CreateEmbedding(embedding);
        if (results.IsLeft)
        {
            // Handle the error has you want
            results.LeftToList();
        }
        
        if(results.IsRight)
        {
            // Handle the success has you want
            results.RightToList();
        }
        
        MistralClient.Build(API_KEY)
            .CreateEmbedding(embedding)
            .Should().BeRightWithLog(c => c.Data.Should().HaveCountGreaterThan(0));
    }
}



