using Interpreter.Context;
using Interpreter.LangParser.Expressions;
using Interpreter.LangParser.Statements;
using Interpreter.Tokenizer;

namespace Interpreter.LangParser
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

        public override void Evaluate(InterpreterExecutionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}