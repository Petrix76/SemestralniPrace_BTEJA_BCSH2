using TypeScriptInterpreter.Context;
using TypeScriptInterpreter.LangParser.Expressions;
using TypeScriptInterpreter.LangParser.Statements;
using TypeScriptInterpreter.Results;

namespace TypeScriptInterpreter.LangParser
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

        public override StatementResult Evaluate(InterpreterExecutionContext context)
        {
            StatementResult statementResult = StatementResult.OkResult();
            object v = Expr.Evaluate(context);

            if (v.GetType() == typeof(int) && VarType == VarType.INT)
            {
                context.Variables.Add(Lexeme, v, VarType);
                return statementResult;
            }

            if (v.GetType() == typeof(double) && VarType == VarType.FLOAT)
            {
                context.Variables.Add(Lexeme, v, VarType);
                return statementResult;
            }

            if (v.GetType() == typeof(string) && VarType == VarType.STRING)
            {
                context.Variables.Add(Lexeme, v, VarType);
                return statementResult;
            }

            throw new ExecutionException($"Cannot assign {v.GetType().Name} to {VarType}");
        }
    }
}