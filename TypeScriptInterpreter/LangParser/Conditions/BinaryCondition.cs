using Interpreter.Context;
using Interpreter.Tokenizer;

namespace Interpreter.LangParser.Conditions;

public class BinaryCondition : Condition
{
    public Token OperatorVar { get; }
    public Evaluatable Right { get; }
    public Evaluatable Left { get; }

    public BinaryCondition(Evaluatable left, Token operatorVar, Evaluatable right)
    {
        Left = left;
        OperatorVar = operatorVar;
        Right = right;
    }

    public override bool Evaluate(InterpreterExecutionContext context)
    {
        switch (OperatorVar.Type)
        {
            case TokenType.LESS: return Left.Evaluate(context) < Right.Evaluate(context);
            case TokenType.GREATER: return Left.Evaluate(context) > Right.Evaluate(context);
            case TokenType.LESS_EQUALS: return Left.Evaluate(context) <= Right.Evaluate(context);
            case TokenType.GREATER_EQUALS: return Left.Evaluate(context) >= Right.Evaluate(context);
            case TokenType.DOUBLE_EQUALS: return Left.Evaluate(context) == Right.Evaluate(context);
            default: throw new ParserException("Wrong token type.");
        }
    }
}
