using Interpreter.Context;
using Interpreter.LangParser.Statements;
using Interpreter.Tokenizer;
using System.Collections.Generic;

namespace Interpreter.LangParser;

public class Function
{
    public string Ident{ get; }
    public FunctionReturnType ReturnType { get; }
    public Block Block { get; }
    public List<FunctionParameter> Parameters { get; }

    public Function(string ident, Block block, List<FunctionParameter> parameters, FunctionReturnType returnType)
    {
        Ident = ident;
        Block = block;
        Parameters = parameters;
        ReturnType = returnType;
    }

    public void Execute(InterpreterExecutionContext context)
    {
        Block.Evaluate(context);
    }
}
