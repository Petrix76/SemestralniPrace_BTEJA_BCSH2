using TypeScriptInterpreter.Context;
using TypeScriptInterpreter.LangParser.Conditions;
using TypeScriptInterpreter.LangParser.Statements;
using System.Collections.Generic;
using TypeScriptInterpreter.Results;

namespace TypeScriptInterpreter.LangParser.Statements;

public class ElseStatement : Statement
{
    public List<Statement> Statements { get; }

    public ElseStatement(List<Statement> statements)
    {
        Statements = statements;
    }

    public override StatementResult Evaluate(InterpreterExecutionContext context)
    {
        StatementResult statementResult = StatementResult.OkResult();
        foreach (var statement in Statements)
        {
            statementResult = statement.Evaluate(context);

            if (!statementResult.IsOk()) return statementResult;
        }

        return statementResult;
    }
}
