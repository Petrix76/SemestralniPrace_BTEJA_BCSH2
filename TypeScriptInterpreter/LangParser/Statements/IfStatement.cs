using TypeScriptInterpreter.Context;
using System.Collections.Generic;
using TypeScriptInterpreter.LangParser.Conditions;
using TypeScriptInterpreter.Results;

namespace TypeScriptInterpreter.LangParser.Statements;

public class IfStatement : Statement
{
    public IfStatement(Condition condition, List<Statement> statements, List<ElseIfStatement> elseIfStatements, ElseStatement? elseStatement)
    {
        Condition = condition;
        Statements = statements;
        ElseIfStatements = elseIfStatements;
        ElseStatement = elseStatement;
    }

    public Condition Condition { get; }
    public List<Statement> Statements { get; }
    public List<ElseIfStatement> ElseIfStatements { get; }
    public ElseStatement? ElseStatement { get; }

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
            return statementResult;
        }

        foreach (ElseIfStatement elseIfStatement in ElseIfStatements)
        {
            if (elseIfStatement.Condition.Evaluate(context))
            {
                statementResult = elseIfStatement.Evaluate(context);
                return statementResult;
            }
        }

        if (ElseStatement is not null)
        {
            statementResult = ElseStatement.Evaluate(context);
        }

        return statementResult;
    }
}
