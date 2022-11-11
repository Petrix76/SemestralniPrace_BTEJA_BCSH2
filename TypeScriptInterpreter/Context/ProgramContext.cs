using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TypeScriptInterpreter.LangParser;
using TypeScriptInterpreter.LangParser.Expressions;

namespace TypeScriptInterpreter.Context;

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

    public object Call(string funcName, List<Expression> parameters, InterpreterExecutionContext context)
    {
        if (HasFunction(funcName))
        {
            return Functions.First(procedure => procedure.Ident.Equals(funcName)).Execute(parameters, context);
        }

        switch (funcName)
        {
            case "in": return ResolveIn(funcName, parameters, context);
            case "out": return ResolveOut(funcName, parameters, context);
            case "readFile": return ResolveReadFile(funcName, parameters, context);
            case "writeFile": return ResolveWriteFile(funcName, parameters, context);
            case "toStringFromFloat": return ResolveToStringFromFloat(funcName, parameters, context);
            case "toIntFromFloat": return ResolveToIntFromFloat(funcName, parameters, context);
            case "toStringFromInt": return ResolveToStringFromInt(funcName, parameters, context);
            case "toFloatFromInt": return ResolveToFloatFromInt(funcName, parameters, context);
            case "toIntFromString": return ResolveToIntFromString(funcName, parameters, context);
            case "toFloatFromString": return ResolveToFloatFromString(funcName, parameters, context);
        }

        throw new InterpreterException("Function is not defined.");
    }

    private object ResolveIn(string funcName, List<Expression> parameters, InterpreterExecutionContext context)
    {
        if (parameters.Count() == 0)
        {
            string? readLine = OutputProvider.In();

            if (readLine != null) return readLine;
            return "";
        }

        throw new ExecutionException($"Function {funcName} has 0 arguments");
    }

    private object ResolveOut(string funcName, List<Expression> parameters, InterpreterExecutionContext context)
    {
        if (parameters.Count() == 1)
        {
            object v = parameters[0].Evaluate(context);
            string? stringValue = v.ToString();
            string printValue = stringValue == null ? "" : stringValue;
            OutputProvider.Out(printValue);
            return new object();
        }

        throw new ExecutionException($"Function {funcName} requires only 1 argument.");
    }

    private object ResolveReadFile(string funcName, List<Expression> parameters, InterpreterExecutionContext context)
    {
        if (parameters.Count() == 1)
        {
            object v = parameters[0].Evaluate(context);
            string? nullableStringValue = v.ToString();
            string path = nullableStringValue == null ? "" : nullableStringValue;

            if (!File.Exists(path)) throw new ExecutionException($"File {path} does not exists.");

            return File.ReadAllText(path);
        }

        throw new ExecutionException($"Function {funcName} requires only 1 argument.");
    }

    private object ResolveWriteFile(string funcName, List<Expression> parameters, InterpreterExecutionContext context)
    {
        if (parameters.Count() == 2)
        {
            object path = parameters[0].Evaluate(context);
            object content = parameters[1].Evaluate(context);

            string? nullablePathValue = path.ToString();
            string pathValue = nullablePathValue == null ? "" : nullablePathValue;

            if (!File.Exists(pathValue)) throw new ExecutionException($"File {pathValue} does not exists.");

            string? nullableContentValue = content.ToString();
            string contentValue = nullableContentValue == null ? "" : nullableContentValue;

            File.WriteAllText(pathValue, contentValue);

            return new object();
        }

        throw new ExecutionException($"Function {funcName} requires only 2 argument.");
    }

    private object ResolveToStringFromFloat(string funcName, List<Expression> parameters, InterpreterExecutionContext context)
    {
        if (parameters.Count() == 1)
        {
            object v = parameters[0].Evaluate(context);

            if (v.GetType() == typeof(double))
            {
                string? nullablePathValue = v.ToString();
                string value = nullablePathValue == null ? "" : nullablePathValue;
                return value;
            }

            throw new ExecutionException($"Function {funcName} requires float.");
        }

        throw new ExecutionException($"Function {funcName} requires only 1 argument.");
    }

    private object ResolveToIntFromFloat(string funcName, List<Expression> parameters, InterpreterExecutionContext context)
    {
        if (parameters.Count() == 1)
        {
            object v = parameters[0].Evaluate(context);

            if (v.GetType() == typeof(double))
            {
                return Convert.ToInt32(v);
            }

            throw new ExecutionException($"Function {funcName} requires float.");
        }

        throw new ExecutionException($"Function {funcName} requires only 1 argument.");
    }

    private object ResolveToStringFromInt(string funcName, List<Expression> parameters, InterpreterExecutionContext context)
    {
        if (parameters.Count() == 1)
        {
            object v = parameters[0].Evaluate(context);

            if (v.GetType() == typeof(int))
            {
                string? nullablePathValue = v.ToString();
                string value = nullablePathValue == null ? "" : nullablePathValue;
                return value;
            }

            throw new ExecutionException($"Function {funcName} requires int.");
        }

        throw new ExecutionException($"Function {funcName} requires only 1 argument.");
    }

    private object ResolveToFloatFromInt(string funcName, List<Expression> parameters, InterpreterExecutionContext context)
    {
        if (parameters.Count() == 1)
        {
            object v = parameters[0].Evaluate(context);

            if (v.GetType() == typeof(int))
            {
                return Convert.ToDouble(v);
            }

            throw new ExecutionException($"Function {funcName} requires int.");
        }

        throw new ExecutionException($"Function {funcName} requires only 1 argument.");
    }

    private object ResolveToIntFromString(string funcName, List<Expression> parameters, InterpreterExecutionContext context)
    {
        if (parameters.Count() == 1)
        {
            object v = parameters[0].Evaluate(context);

            if (v.GetType() == typeof(string))
            {
                return int.Parse((string)v);
            }

            throw new ExecutionException($"Function {funcName} requires string.");
        }

        throw new ExecutionException($"Function {funcName} requires only 1 argument.");
    }

    private object ResolveToFloatFromString(string funcName, List<Expression> parameters, InterpreterExecutionContext context)
    {
        if (parameters.Count() == 1)
        {
            object v = parameters[0].Evaluate(context);

            if (v.GetType() == typeof(string))
            {
                return double.Parse((string)v);
            }

            throw new ExecutionException($"Function {funcName} requires string.");
        }

        throw new ExecutionException($"Function {funcName} requires only 1 argument.");
    }
}
