using Codeblaze.SemanticKernel.Connectors.Ollama;
using Microsoft.SemanticKernel.ChatCompletion;
using Scalar.AspNetCore;
using SemanticKernel.Example.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddKernel()
.AddOllamaChatCompletion("deepseek-r1", "http://localhost:11434");

builder.Services.AddRequestTimeouts();
builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(opt =>
    {
        opt.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });

    app.MapGet("/", context =>
    {
        context.Response.Redirect("scalar/v1");
        return Task.CompletedTask;
    });
}

app.UseRequestTimeouts();

app.MapPost("/chat", async (IChatCompletionService chatCompletionService, ChatModel chat) =>
{
    var response = await chatCompletionService.GetChatMessageContentAsync(chat.Input);
   
    return response.ToString();
}).WithRequestTimeout(TimeSpan.FromMinutes(10));



app.UseHttpsRedirection();
app.Run();