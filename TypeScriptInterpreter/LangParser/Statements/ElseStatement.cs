using Interpreter.Context;
using Interpreter.LangParser.Conditions;
using Interpreter.LangParser.Statements;
using System.Collections.Generic;

namespace Interpreter.LangParser.Statements;

internal class ElseStatement : Statement
{
    public List<Statement> Statements { get; }

    public ElseStatement(List<Statement> statements)
    {
        Statements = statements;
    }

    public override void Evaluate(InterpreterExecutionContext context)
    {
        throw new System.NotImplementedException();
    }
}
