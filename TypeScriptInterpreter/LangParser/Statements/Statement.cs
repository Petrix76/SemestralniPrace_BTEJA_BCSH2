using Interpreter.Context;

namespace Interpreter.LangParser.Statements;

public abstract class Statement
{
    public abstract void Evaluate(InterpreterExecutionContext context);
}
