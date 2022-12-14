using System;

namespace TypeScriptInterpreter;

public static class OutputProvider
{
    public static output Out { get; set; } = (text) => { Console.WriteLine(text); };
    public static error Error { get; set; }
    public static input In { get; set; } = () => Console.ReadLine();
    public delegate void output(string text);
    public delegate void error(Exception e);
    public delegate string? input();
}
