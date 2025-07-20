using System;
using ChatApp.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.SemanticKernel.ChatCompletion;

namespace ChatApp.Services;

public sealed class AIService(IHubContext<AIHub> hubContext, IChatCompletionService chatCompletionService)
{
    public async Task GetMessageStreamAsync(string prompt, string connectionId, CancellationToken cancellationToken)
    {
        await foreach (var response in chatCompletionService.GetStreamingChatMessageContentsAsync(prompt))
        {
            cancellationToken.ThrowIfCancellationRequested();

            await hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", response.ToString());
        }
    }
}
