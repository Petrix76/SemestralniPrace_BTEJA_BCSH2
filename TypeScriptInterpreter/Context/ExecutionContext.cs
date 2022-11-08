using Interpreter.LangParser;
using System.Collections.Generic;

namespace Interpreter.Context;

public class InterpreterExecutionContext
{
    public InterpreterExecutionContext? GlobalContext { get; }
    public Variables Variables { get; }
    public ProgramContext ProgramContext { get; }

    public InterpreterExecutionContext(List<Var> vars, List<Function> functions)
    {
        GlobalContext = null;
        Variables = new Variables(vars);
        ProgramContext = new ProgramContext(functions);
    }

    public InterpreterExecutionContext(List<Var> vars, List<Function> functions, InterpreterExecutionContext context)
    {
        GlobalContext = context;
        Variables = new Variables(vars);
        ProgramContext = new ProgramContext(functions);
    }

    public bool IsGlobal()
    {
        return GlobalContext == null;
    }
}
