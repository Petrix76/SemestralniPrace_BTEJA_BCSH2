using TypeScriptInterpreter.Context;
using TypeScriptInterpreter.LangParser.Conditions;
using TypeScriptInterpreter.LangParser.Statements;
using System.Collections.Generic;
using TypeScriptInterpreter.Results;

namespace TypeScriptInterpreter.LangParser.Statements;

public class ElseIfStatement : Statement
{
    public Condition Condition { get; }
    public List<Statement> Statements { get; }

    public ElseIfStatement(Condition condition, List<Statement> statements)
    {
        Condition = condition;
        Statements = statements;
    }

    public override StatementResult Evaluate(InterpreterExecutionContext context)
    {
        StatementResult statementResult = StatementResult.OkResult();
        if (Condition.Evaluate(context))
        {
            foreach (var statement in Statements)
            {
                statementResult = statement.Evaluate(context);

                if (!statementResult.IsOk()) return statementResult;
            }
        }

        return statementResult;
    }
}
