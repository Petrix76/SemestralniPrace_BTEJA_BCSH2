using Interpreter.Context;
using Interpreter.LangParser.Conditions;
using System.Collections.Generic;

namespace Interpreter.LangParser.Statements;

public class WhileStatement : Statement
{
    public WhileStatement(Condition condition, List<Statement> statements)
    {
        Condition = condition;
        Statements = statements;
    }

    public Condition Condition { get; }
    public List<Statement> Statements { get; }

    public override void Evaluate(InterpreterExecutionContext context)
    {
        while (Condition.Evaluate(context))
        {
            foreach (Statement s in Statements)
            {
                s.Evaluate(context);
            }
        }
    }
}
