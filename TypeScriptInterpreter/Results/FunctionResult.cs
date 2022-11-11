using TypeScriptInterpreter.LangParser.Expressions;
using TypeScriptInterpreter.Results.ResultEnums;

namespace TypeScriptInterpreter.Results;

public class FunctionResult
{
    public Expression? Expr { get; } = null;
    public FunctionResultEnum FunctionResultEnum { get; }

    public FunctionResult(StatementResult statementResult)
    {
        Expr = statementResult.Expr;

        switch (statementResult.StatementResultEnum)
        {
            case StatementResultEnum.RETURN:
                FunctionResultEnum = FunctionResultEnum.RETURNED_VALUE;
                break;
            case StatementResultEnum.EMPTY_RETURN:
                FunctionResultEnum = FunctionResultEnum.EMPTY_RETURN;
                break;
            case StatementResultEnum.OK:
                FunctionResultEnum = FunctionResultEnum.NO_RETURN;
                break;
        }
    }
}
