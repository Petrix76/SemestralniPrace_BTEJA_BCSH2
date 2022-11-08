using Interpreter.Context;
using Interpreter.LangParser.Expressions;

namespace Interpreter.LangParser
{
    internal class GroupExpression : Expression
    {
        private Expression expression;

        public GroupExpression(Expression expression)
        {
            this.expression = expression;
        }

        public override double Evaluate(InterpreterExecutionContext context)
        {
            return expression.Evaluate(context);
        }
    }
}