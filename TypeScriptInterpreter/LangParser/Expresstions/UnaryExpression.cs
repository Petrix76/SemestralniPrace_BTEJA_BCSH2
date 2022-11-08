using Interpreter.Context;
using Interpreter.Tokenizer;

namespace Interpreter.LangParser.Expressions;

public class UnaryExpression : Expression
{
    public Token OperatorVar { get; }
    public Evaluatable Right { get; }

    public UnaryExpression(Token operatorVar, Evaluatable right)
    {
        OperatorVar = operatorVar;
        Right = right;
    }

    public override double Evaluate(InterpreterExecutionContext context)
    {
        if (OperatorVar.Type.Equals(TokenType.MINUS))
        {
            return -Right.Evaluate(context);
        }

        return Right.Evaluate(context);
    }
}
