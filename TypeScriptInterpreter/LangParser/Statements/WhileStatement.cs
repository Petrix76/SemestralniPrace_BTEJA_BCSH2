using TypeScriptInterpreter.Context;
using TypeScriptInterpreter.LangParser.Conditions;
using System.Collections.Generic;
using TypeScriptInterpreter.Results;

namespace TypeScriptInterpreter.LangParser.Statements;

public class WhileStatement : Statement
{
    public WhileStatement(Condition condition, List<Statement> statements)
    {
        Condition = condition;
        Statements = statements;
    }

    public Condition Condition { get; }
    public List<Statement> Statements { get; }

    public override StatementResult Evaluate(InterpreterExecutionContext context)
    {
        StatementResult result = StatementResult.OkResult();
        while (Condition.Evaluate(context))
        {
            foreach (Statement s in Statements)
            {
                result = s.Evaluate(context);

                if (result.StatementResultEnum == Results.ResultEnums.StatementResultEnum.CONTINUE) break;
                if (result.StatementResultEnum == Results.ResultEnums.StatementResultEnum.BREAK) return StatementResult.OkResult();
                if (result.StatementResultEnum == Results.ResultEnums.StatementResultEnum.BREAK) return result;
            }

            if (result.StatementResultEnum == Results.ResultEnums.StatementResultEnum.CONTINUE) continue;
            if (result.StatementResultEnum == Results.ResultEnums.StatementResultEnum.BREAK) break;
        }

        return StatementResult.OkResult();
    }
}
