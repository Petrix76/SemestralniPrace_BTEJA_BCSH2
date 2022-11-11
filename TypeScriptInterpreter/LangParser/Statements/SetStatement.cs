using TypeScriptInterpreter.Context;
using TypeScriptInterpreter.LangParser.Expressions;
using TypeScriptInterpreter.Results;
using TypeScriptInterpreter.Tokenizer;

namespace TypeScriptInterpreter.LangParser.Statements;

public class SetStatement : Statement
{
    public SetStatement(Token identToken, Expression expression)
    {
        Ident = identToken.Lexeme;
        Expression = expression;
    }

    public string Ident { get; }
    public Expression Expression { get; }

    public override StatementResult Evaluate(InterpreterExecutionContext context)
    {
        Variables variables = context.SearchForVariableContext(Ident);
        variables.Set(Ident, Expression.Evaluate(context));

        return StatementResult.OkResult();
    }
}
