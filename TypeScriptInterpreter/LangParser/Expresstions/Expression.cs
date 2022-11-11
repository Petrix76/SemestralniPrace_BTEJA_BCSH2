using TypeScriptInterpreter.Context;

namespace TypeScriptInterpreter.LangParser.Expressions;

public abstract class Expression : Evaluatable
{
    public abstract object Evaluate(InterpreterExecutionContext context);
}
