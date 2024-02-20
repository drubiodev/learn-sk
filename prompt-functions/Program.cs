using Microsoft.Extensions.Configuration;

Console.WriteLine("Prompt Functions");

var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("local.appsettings.json", optional: true, reloadOnChange: true);

IConfigurationRoot configuration = builder.Build();