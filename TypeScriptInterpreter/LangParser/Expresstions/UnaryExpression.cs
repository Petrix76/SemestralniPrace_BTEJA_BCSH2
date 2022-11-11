using TypeScriptInterpreter.Context;
using TypeScriptInterpreter.Tokenizer;

namespace TypeScriptInterpreter.LangParser.Expressions;

public class UnaryExpression : Expression
{
    public Token OperatorVar { get; }
    public Evaluatable Right { get; }

    public UnaryExpression(Token operatorVar, Evaluatable right)
    {
        OperatorVar = operatorVar;
        Right = right;
    }

    public override object Evaluate(InterpreterExecutionContext context)
    {
        object v = Right.Evaluate(context);
        if (OperatorVar.Type.Equals(TokenType.MINUS))
        {
            if (v.GetType() == typeof(int))
            {
                return -(int)v;
            }

            if (v.GetType() == typeof(double))
            {
                return -(double)v;
            }

            throw new ExecutionException("Minus operator cannot be infront of string");
        }

        return v;
    }
}
