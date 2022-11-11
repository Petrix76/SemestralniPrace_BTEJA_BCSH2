using TypeScriptInterpreter.Context;
using TypeScriptInterpreter.Tokenizer;

namespace TypeScriptInterpreter.LangParser.Expressions;

public class IdentExpression : Expression
{
    public string Ident { get; }

    public IdentExpression(Token ident)
    {
        Ident = ident.Lexeme;
    }

    public override object Evaluate(InterpreterExecutionContext context)
    {
        Variables variables = context.SearchForVariableContext(Ident);
        return variables.Get(Ident).Value;    
    }
}
