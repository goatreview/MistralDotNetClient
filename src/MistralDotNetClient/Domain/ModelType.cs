namespace MistralDotNetClient.Domain;

public enum ModelType
{
    Tiny,
    Small,
    Medium,
    Embed
}

public class Model
{    
    public ModelType Type { get; }
    public string Name { get; }

    public static Model Tiny = new(ModelType.Tiny, "mistral-tiny");
    public static Model Small = new(ModelType.Small, "mistral-small");
    public static Model Medium = new(ModelType.Medium, "mistral-medium");
    public static Model Embed = new(ModelType.Embed, "mistral-embed");

    private Model(ModelType modelType, string modelName)
    {
        Type = modelType;
        Name = modelName;
    }
}