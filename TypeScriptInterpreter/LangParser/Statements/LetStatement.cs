using Interpreter.Context;
using Interpreter.LangParser.Expressions;
using Interpreter.LangParser.Statements;

namespace Interpreter.LangParser
{
    internal class LetStatement : Statement
    {
        private string Lexeme { get; }
        private VarType VarType { get; }
        private Expression Expr { get; }

        public LetStatement(string lexeme, VarType v, Expression expr)
        {
            Lexeme = lexeme;
            VarType = v;
            Expr = expr;
        }

        public override void Evaluate(InterpreterExecutionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}