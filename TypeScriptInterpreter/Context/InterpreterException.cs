using System;

namespace TypeScriptInterpreter.Context;

public class InterpreterException : Exception
{
    public InterpreterException(string? message) : base(message)
    {
    }
}
