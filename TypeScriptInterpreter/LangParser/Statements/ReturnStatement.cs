using TypeScriptInterpreter.Context;
using TypeScriptInterpreter.LangParser.Expressions;
using TypeScriptInterpreter.Results;
using TypeScriptInterpreter.Results.ResultEnums;

namespace TypeScriptInterpreter.LangParser.Statements;

internal class ReturnStatement : Statement
{
    public ReturnStatement(Expression? expression)
    {
        Expression = expression;
    }

    public Expression? Expression { get; }

    public override StatementResult Evaluate(InterpreterExecutionContext context)
    {
        if (Expression == null) return new StatementResult(StatementResultEnum.EMPTY_RETURN);
        return new StatementResult(Expression, StatementResultEnum.RETURN);
    }
}
