using Interpreter.Context;
using Interpreter.LangParser.Expressions;
using Interpreter.Tokenizer;
using System.Collections.Generic;

namespace Interpreter.LangParser.Statements;

public class CallExpression : Expression
{
    public CallExpression(string lexeme, List<Expression> parameters)
    {
        Ident = lexeme;
        Parameters = parameters;
    }

    public string Ident { get; }
    public List<Expression> Parameters { get; }

    public override double Evaluate(InterpreterExecutionContext context)
    {
        while (!context.ProgramContext.HasFunction(Ident) && context.GlobalContext is not null)
        {
            context = context.GlobalContext;
        }

        context.ProgramContext.Call(Ident, context);

        return 0.0;
    }
}
