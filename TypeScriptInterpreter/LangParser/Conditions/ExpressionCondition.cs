using TypeScriptInterpreter.Context;
using TypeScriptInterpreter.LangParser.Conditions;

namespace TypeScriptInterpreter.LangParser
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
            object v = expr.Evaluate(context);

            if (v == null) return false;
            return true;
        }
    }
}