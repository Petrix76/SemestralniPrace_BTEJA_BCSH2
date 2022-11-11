using TypeScriptInterpreter.Context;
using TypeScriptInterpreter.Tokenizer;

namespace TypeScriptInterpreter.LangParser.Expressions;

public class LiteralExpression : Expression
{
    public object Literal { get; }
    public TokenType TokenType { get; }

    public LiteralExpression(object literal, TokenType tokenType)
    {
        Literal = literal;
        TokenType = tokenType;
    }

    public override object Evaluate(InterpreterExecutionContext context)
    {
        return Literal;
    }
}
