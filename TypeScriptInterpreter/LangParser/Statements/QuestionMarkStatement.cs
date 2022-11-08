using Interpreter.Context;
using Interpreter.Tokenizer;
using System;

namespace Interpreter.LangParser.Statements;

public class QuestionMarkStatement : Statement
{
    public QuestionMarkStatement(Token identToken)
    {
        Ident = identToken.Lexeme;
    }

    public string Ident { get; }

    public override void Evaluate(InterpreterExecutionContext context)
    {
        double value;
        if (!double.TryParse(Console.ReadLine(), out value))
        {
            throw new InterpreterException("Value must be number");
        }

        InterpreterExecutionContext globalContext = context;
        while (!globalContext.Variables.HasVar(Ident) && globalContext.GlobalContext is not null)
        {
            globalContext = globalContext.GlobalContext;
        }

        globalContext.Variables.Set(Ident, value);
    }
}
