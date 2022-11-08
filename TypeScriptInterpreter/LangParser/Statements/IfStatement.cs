using Interpreter.Context;
using Interpreter.LangParser.Conditions;
using System.Collections.Generic;

namespace Interpreter.LangParser.Statements;

public class IfStatement : Statement
{
    public IfStatement(Condition condition, List<Statement> statements, List<Statement> elseIfStatements, Statement? elseStatement)
    {
        Condition = condition;
        Statements = statements;
        ElseIfStatements = elseIfStatements;
        ElseStatement = elseStatement;
    }

    public Condition Condition { get; }
    public List<Statement> Statements { get; }
    public List<Statement> ElseIfStatements { get; }
    public Statement? ElseStatement { get; }

    public override void Evaluate(InterpreterExecutionContext context)
    {
        if (Condition.Evaluate(context))
        {
            foreach (Statement s in Statements)
            {
                s.Evaluate(context);
            }
        }
    }
}
