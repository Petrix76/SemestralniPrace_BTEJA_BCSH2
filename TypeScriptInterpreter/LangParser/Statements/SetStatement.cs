using Interpreter.Context;
using Interpreter.LangParser.Expressions;
using Interpreter.Tokenizer;

namespace Interpreter.LangParser.Statements;

public class SetStatement : Statement
{
    public SetStatement(Token identToken, Expression expression)
    {
        Ident = identToken.Lexeme;
        Expression = expression;
    }

    public string Ident { get; }
    public Expression Expression { get; }

    public override void Evaluate(InterpreterExecutionContext context)
    {
        InterpreterExecutionContext globalContext = context;
        while (!globalContext.Variables.HasVar(Ident) && globalContext.GlobalContext is not null)
        {
            globalContext = globalContext.GlobalContext;
        }

        globalContext.Variables.Set(Ident, Expression.Evaluate(context));
    }
}
