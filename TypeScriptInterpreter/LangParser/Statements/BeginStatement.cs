using Interpreter.Context;
using System.Collections.Generic;

namespace Interpreter.LangParser.Statements;

public class BeginStatement : Statement
{
    public BeginStatement(List<Statement> statements)
    {
        Statements = statements;
    }

    public List<Statement> Statements { get; }

    public override void Evaluate(InterpreterExecutionContext context)
    {
        foreach (Statement statement in Statements)
        {
            statement.Evaluate(context);
        }
    }
}
