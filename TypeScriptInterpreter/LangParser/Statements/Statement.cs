using TypeScriptInterpreter.Context;
using TypeScriptInterpreter.Results;

namespace TypeScriptInterpreter.LangParser.Statements;

public abstract class Statement
{
    public abstract StatementResult Evaluate(InterpreterExecutionContext context);
}
