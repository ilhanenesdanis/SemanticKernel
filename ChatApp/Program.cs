using ChatApp.Extensions;
using ChatApp.Hubs;
using ChatApp.Models;
using ChatApp.Services;
using ChatApp.Setups;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.ConfigureOptions<ChatOptionSetup>();

builder.Services.AddChatConfiguration();

builder.Services.AddDefaultCorsPolicy();

builder.Services.AddSingleton<AIService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors();

app.MapHub<AIHub>("ai-hub");

app.MapPost("/chat", async (AIService aiService, ChatRequest chatRequest, CancellationToken cancellationToken)
    => await aiService.GetMessageStreamAsync(chatRequest.prompt, chatRequest.connectionId, cancellationToken));

app.Run();

