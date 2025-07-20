using System;
using ChatApp.Options;
using Microsoft.Extensions.Options;

namespace ChatApp.Setups;

public sealed class ChatOptionSetup(IConfiguration configuration) : IConfigureOptions<ChatOptions>
{
    public void Configure(ChatOptions options)
    {
        configuration.GetSection(nameof(ChatOptions)).Bind(options);
    }
}
