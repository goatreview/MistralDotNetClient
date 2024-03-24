using LanguageExt;

namespace MistralDotNetClient.Domain.Embeddings;

public class EmbeddingBuilder
{
    private readonly List<string> _inputs = new();
    private readonly Model _model = Model.Tiny;

    public EmbeddingBuilder WithInputs(IEnumerable<string> inputs)
    {
        _inputs.AddRange(inputs);
        return this;
    }
    
    public Either<InternalError, Embedding> Create()
    {
        if(_inputs.Count == 0)
            return new InternalError(ErrorReason.InvalidObjectCreation, $"{nameof(Embedding)} must have at least one input");
        return new Embedding(_model, _inputs);
    }
}