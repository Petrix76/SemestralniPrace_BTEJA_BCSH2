using Interpreter.Context;

namespace Interpreter.LangParser.Expressions;

public abstract class Expression : Evaluatable
{
    public abstract double Evaluate(InterpreterExecutionContext context);
}
