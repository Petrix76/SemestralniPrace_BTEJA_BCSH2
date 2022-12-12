using TypeScriptInterpreter.Context;
using System.Collections.Generic;
using TypeScriptInterpreter.LangParser.Statements;
using TypeScriptInterpreter.Results;
using TypeScriptInterpreter.Results.ResultEnums;

namespace TypeScriptInterpreter.LangParser;

public class Block
{
    public List<Function> Functions { get; private set; }
    public List<Statement> Statements { get; }

    public Block(List<Statement> statements, List<Function> functions)
    {
        Statements = statements;
        Functions = functions;
    }

    public void Evaluate()
    {
        InterpreterExecutionContext context = new InterpreterExecutionContext(Functions);
        
        foreach (var statement in Statements)
        {
            StatementResult result = statement.Evaluate(context);

            if (result.StatementResultEnum == StatementResultEnum.RETURN) throw new ExecutionException("Cannot return value in program block");
            if (result.StatementResultEnum == StatementResultEnum.CONTINUE || result.StatementResultEnum == StatementResultEnum.BREAK) throw new ExecutionException("Cannot use break or continue out of loop");
            if (result.StatementResultEnum == StatementResultEnum.EMPTY_RETURN) return;
        }
    }

    public FunctionResult Evaluate(InterpreterExecutionContext newContext)
    {
        StatementResult result = StatementResult.OkResult();
        newContext.ProgramContext = new ProgramContext(Functions);
        foreach (var statement in Statements)
        {
            result = statement.Evaluate(newContext);
            if (result.StatementResultEnum == StatementResultEnum.CONTINUE || result.StatementResultEnum == StatementResultEnum.BREAK) throw new ExecutionException("Cannot use break or continue out of loop");
            if (result.StatementResultEnum == StatementResultEnum.RETURN || result.StatementResultEnum == StatementResultEnum.EMPTY_RETURN) return new FunctionResult(result);
        }

        return new FunctionResult(result);
    }
}
