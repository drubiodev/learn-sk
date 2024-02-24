// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.KernelMemory;

var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("local.appsettings.json", optional: true, reloadOnChange: true);

IConfigurationRoot configuration = builder.Build();


Console.WriteLine("Hello, Microsoft Semantic Kernel - 1.0 - Kernel Memory - Demo");

var embeddingAzureConfig = CreateAzureOpenAIConfig(configuration["MODEL_EMBEDDING"], AzureOpenAIConfig.APITypes.EmbeddingGeneration);

var textAzureConfig = CreateAzureOpenAIConfig(configuration["MODEL_AI"], AzureOpenAIConfig.APITypes.ChatCompletion);

var kernelMemory = new KernelMemoryBuilder()
    .WithAzureOpenAITextEmbeddingGeneration(embeddingAzureConfig)
    .WithAzureOpenAITextGeneration(textAzureConfig)
    .WithSimpleVectorDb()
    .Build();


await kernelMemory.ImportDocumentAsync("NET.pdf");

var result = await kernelMemory.AskAsync("Can you explain .NET?");

Console.WriteLine(result.Result);

Console.Read();

AzureOpenAIConfig CreateAzureOpenAIConfig(string deploymentName, AzureOpenAIConfig.APITypes apiTypes)
{
    return new AzureOpenAIConfig()
    {
        APIKey = configuration["API_KEY"],
        Deployment = deploymentName,
        Endpoint = configuration["ENDPOINT"],
        APIType = apiTypes,
        Auth = AzureOpenAIConfig.AuthTypes.APIKey
    };
}
