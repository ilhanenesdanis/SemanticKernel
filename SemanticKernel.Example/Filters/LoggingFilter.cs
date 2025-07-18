using System;
using Microsoft.SemanticKernel;

namespace SemanticKernel.Example.Filters;

public sealed class LoggingFilter : IFunctionInvocationFilter
{
    public async Task OnFunctionInvocationAsync(FunctionInvocationContext context, Func<FunctionInvocationContext, Task> next)
    {
        Console.WriteLine($"Running plugin name: {context.Function.PluginName} | Function Name: {context.Function.Name}");
        await next(context);
        Console.WriteLine($"Was run plugin name: {context.Function.PluginName} | Function Name: {context.Function.Name}");
    }
}
