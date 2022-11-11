using TypeScriptInterpreter.Context;

namespace TypeScriptInterpreter.LangParser
{
    public interface Evaluatable
    {
        object Evaluate(InterpreterExecutionContext context);
    }
}