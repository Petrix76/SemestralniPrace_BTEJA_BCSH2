using System;

namespace TypeScriptInterpreter;

public class ExecutionException : Exception
{
    public ExecutionException(string? message) : base(message)
    {
    }
}
