using Interpreter.Context;
using Interpreter.Tokenizer;

namespace Interpreter.LangParser.Expressions;

public class IdentExpression : Expression
{
    public string Ident { get; }

    public IdentExpression(Token ident)
    {
        Ident = ident.Lexeme;
    }

    public override double Evaluate(InterpreterExecutionContext context)
    {
        while (!context.Variables.HasVar(Ident) && context.GlobalContext is not null)
        {
            context = context.GlobalContext;
        }

        return context.Variables.Get(Ident);
    }
}
