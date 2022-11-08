using Interpreter.Context;
using Interpreter.Tokenizer;

namespace Interpreter.LangParser.Expressions;

public class BinaryExpression : Expression
{
    public Token OperatorVar { get; }
    public Evaluatable Right { get; }
    public Evaluatable Left { get; }

    public BinaryExpression(Evaluatable left, Token operatorVar, Evaluatable right)
    {
        Left = left;
        OperatorVar = operatorVar;
        Right = right;
    }

    public override double Evaluate(InterpreterExecutionContext context)
    {
        switch (OperatorVar.Type)
        {
            case TokenType.PLUS: return Left.Evaluate(context) + Right.Evaluate(context);
            case TokenType.MINUS: return Left.Evaluate(context) - Right.Evaluate(context);
            case TokenType.STAR: return Left.Evaluate(context) * Right.Evaluate(context);
            case TokenType.DIVIDE: return Left.Evaluate(context) / Right.Evaluate(context);
            default: throw new ParserException("Wrong token type.");
        }
    }
}
