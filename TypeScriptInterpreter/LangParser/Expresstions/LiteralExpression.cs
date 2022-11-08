using Interpreter.Context;
using Interpreter.Tokenizer;

namespace Interpreter.LangParser.Expressions;

public class LiteralExpression : Expression
{
    public object Literal { get; }
    public TokenType TokenType { get; }

    public LiteralExpression(object literal, TokenType tokenType)
    {
        Literal = literal;
        TokenType = tokenType;
    }

    public override double Evaluate(InterpreterExecutionContext context)
    {
        return (double) Literal;
    }
}
