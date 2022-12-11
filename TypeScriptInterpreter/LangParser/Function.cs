using TypeScriptInterpreter.Context;
using System.Collections.Generic;
using TypeScriptInterpreter.LangParser.Expressions;
using TypeScriptInterpreter.Results;
using TypeScriptInterpreter.Results.ResultEnums;
using System.Linq;
using System;

namespace TypeScriptInterpreter.LangParser;

public class Function
{
    public string Ident{ get; }
    public FunctionReturnType ReturnType { get; }
    public Block Block { get; }
    public List<FunctionParameter> Parameters { get; }

    public Function(string ident, Block block, List<FunctionParameter> parameters, FunctionReturnType returnType)
    {
        Ident = ident;
        Block = block;
        Parameters = parameters;
        ReturnType = returnType;
    }

    public object Execute(List<Expression> parameters, InterpreterExecutionContext context)
    {
        InterpreterExecutionContext newContext = new InterpreterExecutionContext(context);

        if (Parameters.Count() != parameters.Count())
        {
            throw new ExecutionException($"Function {Ident} has {Parameters.Count()} provided {parameters.Count()} parameters");
        }

        for (int i = 0; i < Parameters.Count(); i++)
        {
            FunctionParameter functionParameter = Parameters[i];
            Expression expression = parameters[i];

            object paramValue = expression.Evaluate(context);

            CheckTypes(i + 1, functionParameter, paramValue);

            newContext.Variables.Add(functionParameter.Lexeme, paramValue, functionParameter.VarType);
        }

        FunctionResult functionResult = Block.Evaluate(newContext);

        if (functionResult.FunctionResultEnum == FunctionResultEnum.NO_RETURN && ReturnType == FunctionReturnType.VOID)
        {
            return new object();
        }

        if (functionResult.FunctionResultEnum == FunctionResultEnum.EMPTY_RETURN && ReturnType == FunctionReturnType.VOID)
        {
            return new object();
        }

        CheckBadReturn(functionResult);

        if (functionResult.Expr is null) throw new ExecutionException("ups something went wrong.");

        object returnValue = functionResult.Expr.Evaluate(newContext);

        if (returnValue.GetType() == typeof(string) && ReturnType == FunctionReturnType.STRING)
        {
            return returnValue;
        }

        if (returnValue.GetType() == typeof(int) && ReturnType == FunctionReturnType.INT)
        {
            return returnValue;
        }

        return returnValue;
    }

    private void CheckBadReturn(FunctionResult functionResult)
    {
        if (functionResult.FunctionResultEnum == FunctionResultEnum.NO_RETURN && ReturnType == FunctionReturnType.INT)
        {
            throw new ExecutionException($"Function '{Ident}' should return {FunctionReturnType.INT}");
        }

        if (functionResult.FunctionResultEnum == FunctionResultEnum.NO_RETURN && ReturnType == FunctionReturnType.FLOAT)
        {
            throw new ExecutionException($"Function '{Ident}' should return {FunctionReturnType.FLOAT}");
        }

        if (functionResult.FunctionResultEnum == FunctionResultEnum.NO_RETURN && ReturnType == FunctionReturnType.STRING)
        {
            throw new ExecutionException($"Function '{Ident}' should return {FunctionReturnType.STRING}");
        }

        if (functionResult.FunctionResultEnum == FunctionResultEnum.EMPTY_RETURN && ReturnType == FunctionReturnType.INT)
        {
            throw new ExecutionException($"Function '{Ident}' should return {FunctionReturnType.INT}");
        }

        if (functionResult.FunctionResultEnum == FunctionResultEnum.EMPTY_RETURN && ReturnType == FunctionReturnType.FLOAT)
        {
            throw new ExecutionException($"Function '{Ident}' should return {FunctionReturnType.FLOAT}");
        }

        if (functionResult.FunctionResultEnum == FunctionResultEnum.EMPTY_RETURN && ReturnType == FunctionReturnType.STRING)
        {
            throw new ExecutionException($"Function '{Ident}' should return {FunctionReturnType.STRING}");
        }
    }

    private void CheckTypes(int i, FunctionParameter functionParameter, object param)
    {
        if (param.GetType() == typeof(int) && functionParameter.VarType == VarType.STRING)
        {
            throw new ExecutionException($"Function '{Ident}': {i}. parameter is type of string, but got int.");
        }

        if (param.GetType() == typeof(double) && functionParameter.VarType == VarType.STRING)
        {
            throw new ExecutionException($"Function '{Ident}': {i}. parameter is type of string, but got float.");
        }

        if (param.GetType() == typeof(string) && functionParameter.VarType == VarType.INT)
        {
            throw new ExecutionException($"Function '{Ident}': {i}. parameter is type of int, but got float.");
        }

        if (param.GetType() == typeof(double) && functionParameter.VarType == VarType.INT)
        {
            throw new ExecutionException($"Function '{Ident}': {i}. parameter is type of int, but got float.");
        }

        if (param.GetType() == typeof(string) && functionParameter.VarType == VarType.FLOAT)
        {
            throw new ExecutionException($"Function '{Ident}': {i}. parameter is type of float, but got string.");
        }

        if (param.GetType() == typeof(int) && functionParameter.VarType == VarType.FLOAT)
        {
            throw new ExecutionException($"Function '{Ident}': {i}. parameter is type of float, but got int.");
        }
    }
}
