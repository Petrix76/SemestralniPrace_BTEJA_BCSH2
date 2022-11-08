using Interpreter.Context;

namespace Interpreter.LangParser.Conditions;

public abstract class Condition
{
    public abstract bool Evaluate(InterpreterExecutionContext context);
}
