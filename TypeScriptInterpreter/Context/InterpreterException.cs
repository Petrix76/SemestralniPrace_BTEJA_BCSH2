using System;

namespace Interpreter.Context;

public class InterpreterException : Exception
{
    public InterpreterException(string? message) : base(message)
    {
    }
}
