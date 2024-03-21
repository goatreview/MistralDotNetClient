# Mistral .NET Client

A simple C# .NET wrapper library to use with Mistral's API. This is an unofficial wrapper library around the Mistral API. I am not affiliated with Mistral and this library is not endorsed or supported by them.

## Requirements

## Getting started

### Install from Nuget

You can install directly from Nuget : `dotnet add package GoatReview.MistralDotNetClient`

### Client

The client is the main entry point to the Mistral API. You can create a new client by calling the `Build` method with your API key.

```csharp   
MistralClient.Build(API_KEY);
```

If you don't have API key, you can create one direclty from the [Mistral website]( https://console.mistral.ai/api-keys).

## Features

Before starting to explain the features, it's important to understand the main concepts of monads and how they are used in this library.

Each feature return an object of `Either<InternalError, XXX>` where `XXX` is the expected return type of the feature. The `InternalError` object contains the error reason and the error message of the request.

```csharp

```


### Completions




### Embeddings

```csharp
MistralClient.Build(API_KEY)
    .CreateEmbedding(embedding);
```

## Licence
