using Interpreter.LangParser;
using System.Collections.Generic;
using System.Linq;

namespace Interpreter.Context;

public class Variables
{
    public List<Var> Vars { get; private set; }

    public Variables(List<Var> vars)
    {
        Vars = vars;
    }

    public bool HasVar(string varName)
    {
        return Vars.Exists(var => var.Ident.Equals(varName));
    }

    public double Get(string varName)
    {
        if (HasVar(varName))
        {
            return (double) Vars.First(var => var.Ident.Equals(varName)).Value;
        }

        throw new InterpreterException("Variable does not exist in this context.");
    }

    public void Set(string varName, double newValue)
    {
        if (HasVar(varName))
        {
            Vars.First(var => var.Ident.Equals(varName)).Value = newValue;
            return;
        }

        throw new InterpreterException("Variable does not exist in this context.");
    }

    public void Add(string varName, object value, VarType varType)
    {
        Vars.Add(new Var(varName, value, varType));
    }
}

