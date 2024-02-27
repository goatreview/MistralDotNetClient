namespace MistralDotNetClient.Domain.Embeddings;

public class Embedding
{
    public Model Model { get; }
    public List<string> Inputs { get; }

    internal Embedding(Model model, List<string> inputs)
    {
        Model = model;
        Inputs = inputs;
    }

    public static EmbeddingBuilder Build()
    {
        return new EmbeddingBuilder();
    }
}