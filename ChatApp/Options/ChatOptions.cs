using System;

namespace ChatApp.Options;

public sealed class ChatOptions
{
    public required string ModelId { get; set; }
    public required string ApiUrl { get; set; }
    public required string ApiKey { get; set; }
}
