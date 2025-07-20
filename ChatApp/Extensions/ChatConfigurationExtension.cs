using System;
using System.ClientModel;
using ChatApp.Options;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using OpenAI;

namespace ChatApp.Extensions;

public static class ChatConfigurationExtension
{
    public static void AddChatConfiguration(this IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<ChatOptions>>().Value;

        services.AddOpenAIChatCompletion(
            modelId: options.ModelId,
            openAIClient: new OpenAIClient(
                credential: new ApiKeyCredential(options.ApiKey),
                options: new OpenAIClientOptions
                {
                    Endpoint = new Uri(options.ApiUrl)
                }
            )
        );
    }
}
