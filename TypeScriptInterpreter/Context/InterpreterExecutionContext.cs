using System.Collections.Generic;
using TypeScriptInterpreter.LangParser;

namespace TypeScriptInterpreter.Context;

public class InterpreterExecutionContext
{
    public InterpreterExecutionContext? GlobalContext { get; }
    public Variables Variables { get; }
    public ProgramContext ProgramContext { get; set; }

    private List<string> libraryFunctions = new List<string>()
    {
        "in", "out", "readFile", "writeFile", "toStringFromFloat", "toIntFromFloat", "toStringFromInt", "toFloatFromInt", "toIntFromString", "toFloatFromString"
    };

    public InterpreterExecutionContext(List<Function> functions)
    {
        GlobalContext = null;
        Variables = new Variables();
        ProgramContext = new ProgramContext(functions);
    }

    public InterpreterExecutionContext(InterpreterExecutionContext context)
    {
        if (context.GlobalContext == null)
        {
            GlobalContext = context;
        } else
        {
            GlobalContext = context.GlobalContext;
        }
        
        Variables = new Variables();
    }

    public Variables SearchForVariableContext(string varName)
    {
        if (Variables.HasVar(varName))
        {
            return Variables;
        }

        if (GlobalContext is not null && GlobalContext.Variables.HasVar(varName))
        {
            return GlobalContext.Variables;
        }

        throw new ExecutionException($"Variable '{varName}' does not exist.");
    }

    public ProgramContext SearchForFunctionContext(string funcName)
    {
        if (libraryFunctions.Contains(funcName))
        {
            return ProgramContext;
        }

        if (ProgramContext.HasFunction(funcName))
        {
            return ProgramContext;
        }

        if (GlobalContext is not null && GlobalContext.ProgramContext.HasFunction(funcName))
        {
            return GlobalContext.ProgramContext;
        }

        throw new ExecutionException($"Function '{funcName}' does not exist.");
    }

    public bool IsGlobal()
    {
        return GlobalContext == null;
    }
}
