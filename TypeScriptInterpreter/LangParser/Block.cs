using Interpreter.Context;
using Interpreter.LangParser.Statements;
using System.Collections.Generic;

namespace Interpreter.LangParser;

public class Block
{
    public List<Var> Vars { get; private set; }
    public List<Function> Functions { get; private set; }
    public List<Statement> BlockStatements { get; private set; }
    public List<Statement> Statements { get; }

    public Block(List<Var> vars, List<Function> functions, List<Statement> statements)
    {
        Vars = vars;
        Functions = functions;
        BlockStatements = statements;
    }

    public Block(List<Statement> statements, List<Function> functions)
    {
        Statements = statements;
        Functions = functions;
    }

    public void Evaluate()
    {
        InterpreterExecutionContext context = new InterpreterExecutionContext(Vars, Functions);
        foreach (var statement in BlockStatements)
        {
            statement.Evaluate(context);
        }
    }

    public void Evaluate(InterpreterExecutionContext context)
    {
        InterpreterExecutionContext newContext = new InterpreterExecutionContext(Vars, Functions, context);
        foreach (var statement in BlockStatements)
        {
            statement.Evaluate(newContext);
        }
    }
}
