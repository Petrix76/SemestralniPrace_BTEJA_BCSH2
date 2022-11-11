using System;

namespace TypeScriptInterpreter.LangParser;

public class ParserException : Exception
{
    public ParserException(string? message) : base(message)
    {
    }
}
