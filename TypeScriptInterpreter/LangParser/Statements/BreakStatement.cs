using TypeScriptInterpreter.Context;
using TypeScriptInterpreter.LangParser.Statements;
using TypeScriptInterpreter.Results;
using TypeScriptInterpreter.Results.ResultEnums;

namespace TypeScriptInterpreter.LangParser.Statements;

public class BreakStatement : Statement
{
    public override StatementResult Evaluate(InterpreterExecutionContext context)
    {
        return new StatementResult(StatementResultEnum.BREAK);
    }
}
