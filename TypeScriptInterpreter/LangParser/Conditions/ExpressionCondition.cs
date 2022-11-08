using Interpreter.Context;
using Interpreter.LangParser.Conditions;

namespace Interpreter.LangParser
{
    internal class ExpressionCondition : Condition
    {
        private Evaluatable expr;

        public ExpressionCondition(Evaluatable expr)
        {
            this.expr = expr;
        }

        public override bool Evaluate(InterpreterExecutionContext context)
        {
            double v = expr.Evaluate(context);

            if (v == 0) return false;
            return true;
        }
    }
}