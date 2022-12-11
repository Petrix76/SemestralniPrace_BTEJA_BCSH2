using TypeScriptInterpreter.LangParser;
using TypeScriptInterpreter.Tokenizer;
using System.Collections.Generic;
using System.IO;
using System;

namespace TypeScriptInterpreter;

public class Interpreter
{
    public void Interpret(string? path)
    {
        if (path == null) return;
        if (File.Exists(path))
        {
            try
            {
                string readText = File.ReadAllText(path);
                Lexer lexer = new Lexer(readText);
                List<Token> tokens = lexer.GetTokens();

                Parser parser = new Parser(tokens);
                Block program = parser.Parse();
                
                program.Evaluate();
            }
            catch (Exception e)
            {
                OutputProvider.Error(e);
            }
        }
    } 
}
