using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

Console.WriteLine("Prompt Functions");

var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("local.appsettings.json", optional: true, reloadOnChange: true);

IConfigurationRoot configuration = builder.Build();

Console.WriteLine("Enter the text with city information");

var userInput = Console.ReadLine();

var kernel = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion(configuration["MODEL_AI"], configuration["ENDPOINT"], configuration["API_KEY"])
    .Build();

//Import Prompt Functions
var plugInDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Plugins", "location");
kernel.ImportPluginFromPromptDirectory(plugInDirectory, "location");

//Get the function from location plugin
var cityKernelFunction = kernel.Plugins.GetFunction("location", "city");

//Kernel arguments

var kernelArguments = new KernelArguments()
    {
        {
            "input", userInput
        }
    };


var functionResults = await kernel.InvokeAsync(cityKernelFunction, kernelArguments);

Console.WriteLine(functionResults.GetValue<string>());

Console.Read();
