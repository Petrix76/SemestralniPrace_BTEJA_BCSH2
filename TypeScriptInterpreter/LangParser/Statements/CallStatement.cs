using TypeScriptInterpreter.Context;
using TypeScriptInterpreter.LangParser.Expressions;
using TypeScriptInterpreter.LangParser.Statements;
using TypeScriptInterpreter.Results;

namespace TypeScriptInterpreter.LangParser
{
    internal class CallStatement : Statement
    {
        private string Ident { get; }
        private Expression Expression { get; }

        public CallStatement(string ident, Expression expression)
        {
            Ident = ident;
            Expression = expression;
        }

        public override StatementResult Evaluate(InterpreterExecutionContext context)
        {
            Expression.Evaluate(context);
            return StatementResult.OkResult();
        }
    }
}