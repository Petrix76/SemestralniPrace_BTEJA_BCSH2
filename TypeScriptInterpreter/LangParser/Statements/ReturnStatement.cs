using Interpreter.Context;
using Interpreter.LangParser.Expressions;

namespace Interpreter.LangParser.Statements;

internal class ReturnStatement : Statement
{
    public ReturnStatement(Expression? expression)
    {
        Expression = expression;
    }

    public Expression? Expression { get; }

    public override void Evaluate(InterpreterExecutionContext context)
    {
        throw new System.NotImplementedException();
    }
}
