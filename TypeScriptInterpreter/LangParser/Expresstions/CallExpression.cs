using TypeScriptInterpreter.Context;
using TypeScriptInterpreter.LangParser.Expressions;
using System.Collections.Generic;
using TypeScriptInterpreter.Results;

namespace TypeScriptInterpreter.LangParser.Statements;

public class CallExpression : Expression
{
    public CallExpression(string lexeme, List<Expression> parameters)
    {
        Ident = lexeme;
        Parameters = parameters;
    }

    public string Ident { get; }
    public List<Expression> Parameters { get; }

    public override object Evaluate(InterpreterExecutionContext context)
    {
        ProgramContext programContext = context.SearchForFunctionContext(Ident);
        return programContext.Call(Ident, Parameters, context);
    }
}
