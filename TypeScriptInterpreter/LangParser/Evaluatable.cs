using Interpreter.Context;

namespace Interpreter.LangParser
{
    public interface Evaluatable
    {
        double Evaluate(InterpreterExecutionContext context);
    }
}