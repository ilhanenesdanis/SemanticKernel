using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// var builder = Host.CreateApplicationBuilder();

// builder.Services.AddChatClient(new OllamaChatClient(new Uri("http://localhost:11434"), "deepseek-r1"));

// var app = builder.Build();


// var chatClient = app.Services.GetRequiredService<IChatClient>();

// var chatCompetion = await chatClient.GetResponseAsync("1+1=?");

// System.Console.WriteLine(chatCompetion.Messages.FirstOrDefault()?.Text ?? "olmadı");

var builder = Host.CreateApplicationBuilder();

builder.Services.AddChatClient(new OllamaChatClient(new Uri("http://localhost:11434"), "deepseek-r1"));

var app = builder.Build();


var chatClient = app.Services.GetRequiredService<IChatClient>();

var chatHistory = new List<ChatMessage>();
while (true)
{
    System.Console.WriteLine("ask me question... ");

    var prompt = Console.ReadLine();

    chatHistory.Add(new ChatMessage(ChatRole.User, prompt));

    Console.WriteLine("Answer ");

    var chatResponse = string.Empty;

    await foreach (var response in chatClient.GetStreamingResponseAsync(chatHistory))
    {
        System.Console.Write(response.Text);

        chatResponse += response.Text;
    }
    chatHistory.Add(new ChatMessage(ChatRole.Assistant, chatResponse));
    Console.ReadLine();
}

