using Codeblaze.SemanticKernel.Connectors.Ollama;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Scalar.AspNetCore;
using SemanticKernel.Example.Filters;
using SemanticKernel.Example.Models;
using SemanticKernel.Example.Plugins;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddKernel()
.AddOllamaChatCompletion("deepseek-r1", "http://localhost:11434")
.Plugins.AddFromType<CalculationPlugin>();

builder.Services.AddSingleton<IFunctionInvocationFilter, LoggingFilter>();


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

app.MapPost("/chat/{number1}/{number2}", async (Kernel kernel, int number1, int number2) =>
{
    var arguments = new KernelArguments
    {
        ["num1"] = number1,
        ["num2"] = number2,
    };

    var addResult = await kernel.InvokeAsync(nameof(CalculationPlugin), nameof(CalculationPlugin.Add), arguments);

    return addResult.GetValue<int>();

}).WithRequestTimeout(TimeSpan.FromMinutes(10));





app.UseHttpsRedirection();
app.Run();