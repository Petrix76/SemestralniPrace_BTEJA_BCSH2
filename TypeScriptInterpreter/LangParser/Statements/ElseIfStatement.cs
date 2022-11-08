using Interpreter.Context;
using Interpreter.LangParser.Conditions;
using Interpreter.LangParser.Statements;
using System.Collections.Generic;

namespace Interpreter.LangParser.Statements;

internal class ElseIfStatement : Statement
{
    private Condition condition;
    private List<Statement> statements;

    public ElseIfStatement(Condition condition, List<Statement> statements)
    {
        this.condition = condition;
        this.statements = statements;
    }

    public override void Evaluate(InterpreterExecutionContext context)
    {
        throw new System.NotImplementedException();
    }
}
