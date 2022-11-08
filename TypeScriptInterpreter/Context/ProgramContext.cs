using Interpreter.LangParser;
using Interpreter.LangParser.Statements;
using System.Collections.Generic;
using System.Linq;

namespace Interpreter.Context;

public class ProgramContext
{
    public List<Function> Functions { get; private set; }

    public ProgramContext(List<Function> functions)
    {
        Functions = functions;
    }

    public bool HasFunction(string procedureName)
    {
        return Functions.Exists(procedure => procedure.Ident.Equals(procedureName));
    }

    public void Call(string procedureName, InterpreterExecutionContext context)
    {
        if (HasFunction(procedureName))
        {
            Functions.First(procedure => procedure.Ident.Equals(procedureName)).Execute(context);
            return;
        }

        throw new InterpreterException("Method is not defined.");
    }
}
