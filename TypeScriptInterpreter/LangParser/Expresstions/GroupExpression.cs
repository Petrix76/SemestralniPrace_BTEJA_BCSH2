using TypeScriptInterpreter.Context;
using TypeScriptInterpreter.LangParser.Expressions;

namespace TypeScriptInterpreter.LangParser
{
    internal class GroupExpression : Expression
    {
        private Expression expression;

        public GroupExpression(Expression expression)
        {
            this.expression = expression;
        }

        public override object Evaluate(InterpreterExecutionContext context)
        {
            return expression.Evaluate(context);
        }
    }
}