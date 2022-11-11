using System;
using TypeScriptInterpreter.LangParser.Expressions;
using TypeScriptInterpreter.Results.ResultEnums;

namespace TypeScriptInterpreter.Results;

public class StatementResult
{
    public Expression? Expr { get; } = null;
    public StatementResultEnum StatementResultEnum { get; }

    public static StatementResult OkResult()
    {
        return new StatementResult(StatementResultEnum.OK);
    }

    public StatementResult(StatementResultEnum statementResultEnum)
    {
        StatementResultEnum = statementResultEnum;
    }

    public StatementResult(Expression? expr, StatementResultEnum statementResultEnum)
    {
        Expr = expr;
        StatementResultEnum = statementResultEnum;
    }

    public bool IsOk()
    {
        return StatementResultEnum == StatementResultEnum.OK;
    }
}
