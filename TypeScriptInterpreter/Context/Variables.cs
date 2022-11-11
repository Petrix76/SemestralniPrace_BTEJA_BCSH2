using System.Collections.Generic;
using System.Linq;
using TypeScriptInterpreter.LangParser;

namespace TypeScriptInterpreter.Context;

public class Variables
{
    public List<Var> Vars { get; private set; } = new List<Var>();

    public bool HasVar(string varName)
    {
        return Vars.Exists(var => var.Ident.Equals(varName));
    }

    public Var Get(string varName)
    {
        if (HasVar(varName))
        {
            return Vars.First(var => var.Ident.Equals(varName));
        }

        throw new InterpreterException("Variable does not exist in this context.");
    }

    public void Set(string varName, object newValue)
    {
        if (HasVar(varName))
        {
            Var variable = Get(varName);

            if (newValue.GetType() == typeof(string) && variable.Type == VarType.STRING)
            {
                Vars.First(var => var.Ident.Equals(varName)).Value = newValue;
                return;
            }

            if (newValue.GetType() == typeof(int) && variable.Type == VarType.INT)
            {
                Vars.First(var => var.Ident.Equals(varName)).Value = newValue;
                return;
            }

            if (newValue.GetType() == typeof(double) && variable.Type == VarType.FLOAT)
            {
                Vars.First(var => var.Ident.Equals(varName)).Value = newValue;
                return;
            }

            throw new ExecutionException($"Cannot assign {newValue.GetType().Name} to {variable.Type}");
        }

        throw new InterpreterException("Variable does not exist in this context.");
    }

    public void Add(string varName, object value, VarType varType)
    {
        Vars.Add(new Var(varName, value, varType));
    }
}

