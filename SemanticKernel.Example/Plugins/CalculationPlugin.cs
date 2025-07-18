using System;
using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace SemanticKernel.Example.Plugins;

public sealed class CalculationPlugin
{
    [KernelFunction("Add")]
    [Description("Performs addition on two numeric values")]
    public int Add(int num1, int num2) => num1 + num2;
}
