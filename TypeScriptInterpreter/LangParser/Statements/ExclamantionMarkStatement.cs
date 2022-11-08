using Interpreter.Context;
using Interpreter.LangParser.Expressions;
using System;

namespace Interpreter.LangParser.Statements;

public class ExclamationMarkStatement : Statement
{
    public ExclamationMarkStatement(Expression expression)
    {
        Expression = expression;
    }

    public Expression Expression { get; }

    public override void Evaluate(InterpreterExecutionContext context)
    {
        Console.WriteLine(Expression.Evaluate(context));
    }
}
