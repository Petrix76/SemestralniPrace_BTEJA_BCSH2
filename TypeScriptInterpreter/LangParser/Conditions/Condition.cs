using TypeScriptInterpreter.Context;

namespace TypeScriptInterpreter.LangParser.Conditions;

public abstract class Condition
{
    public abstract bool Evaluate(InterpreterExecutionContext context);
}
