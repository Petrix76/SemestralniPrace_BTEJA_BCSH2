using Interpreter.LangParser;
using Interpreter.Tokenizer;
using System.Collections.Generic;
using System.IO;

namespace TypeScriptInterpreter;

public class Interpreter
{
    public void Interpret()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "kod.txt");

        if (File.Exists(path))
        {
            string readText = File.ReadAllText(path);
            Lexer lexer = new Lexer(readText);
            List<Token> tokens = lexer.GetTokens();

            Parser parser = new Parser(tokens);
            Block program = parser.Parse();
            //program.Evaluate();
        }
    } 
}
