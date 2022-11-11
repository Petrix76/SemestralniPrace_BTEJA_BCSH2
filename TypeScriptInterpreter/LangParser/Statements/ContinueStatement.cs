using TypeScriptInterpreter.Context;
using TypeScriptInterpreter.Results;
using TypeScriptInterpreter.Results.ResultEnums;

namespace TypeScriptInterpreter.LangParser.Statements;

public class ContinueStatement : Statement
{
    public override StatementResult Evaluate(InterpreterExecutionContext context)
    {
        return new StatementResult(StatementResultEnum.CONTINUE);
    }
}
