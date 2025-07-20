using System;

namespace ChatApp.Models;

public sealed record ChatRequest(string prompt,string connectionId);
