using System;
using TypeScriptInterpreter.Context;
using TypeScriptInterpreter.Tokenizer;

namespace TypeScriptInterpreter.LangParser.Expressions;

public class BinaryExpression : Expression
{
    public Token OperatorVar { get; }
    public Evaluatable Right { get; }
    public Evaluatable Left { get; }

    public BinaryExpression(Evaluatable left, Token operatorVar, Evaluatable right)
    {
        Left = left;
        OperatorVar = operatorVar;
        Right = right;
    }

    public override object Evaluate(InterpreterExecutionContext context)
    {
        object left = Left.Evaluate(context);
        object right = Right.Evaluate(context);

        switch (OperatorVar.Type)
        {
            case TokenType.PLUS: return ResolvePlus(left, right);
            case TokenType.MINUS: return ResolveMinus(left, right);
            case TokenType.STAR: return ResolveStar(left, right);
            case TokenType.DIVIDE: return ResolveDivide(left, right);
            default: throw new ParserException("Wrong token type.");
        }
    }

    private object ResolvePlus(object left, object right)
    {
        CheckDifferentTypes(left, right);

        if (left.GetType() == typeof(string) && right.GetType() == typeof(string))
        {
            return (string)left + (string)right;
        }

        if (left.GetType() == typeof(int) && right.GetType() == typeof(int))
        {
            return (int)left + (int)right;
        }

        return (double)left + (double)right;
    }
    private object ResolveMinus(object left, object right)
    {
        CheckDifferentTypes(left, right);

        if (left.GetType() == typeof(string) && right.GetType() == typeof(string))
        {
            throw new ExecutionException("Cannot do minus operation on two strings");
        }

        if (left.GetType() == typeof(int) && right.GetType() == typeof(int))
        {
            return (int)left - (int)right;
        }

        return (double)left - (double)right;
    }

    private object ResolveStar(object left, object right)
    {
        CheckDifferentTypes(left, right);

        if (left.GetType() == typeof(string) && right.GetType() == typeof(string))
        {
            throw new ExecutionException("Cannot do multiplication operation on two strings");
        }

        if (left.GetType() == typeof(int) && right.GetType() == typeof(int))
        {
            return (int)left * (int)right;
        }

        return (double)left * (double)right;
    }

    private object ResolveDivide(object left, object right)
    {
        CheckDifferentTypes(left, right);

        if (left.GetType() == typeof(string) && right.GetType() == typeof(string))
        {
            throw new ExecutionException("Cannot do division operation on two strings");
        }

        if (left.GetType() == typeof(int) && right.GetType() == typeof(int))
        {
            return (int)left / (int)right;
        }

        return (double)left / (double)right;
    }
    
    private void CheckDifferentTypes(object left, object right)
    {
        if (left.GetType() == typeof(int) && right.GetType() == typeof(double))
        {
            throw new ExecutionException("Cannot do math operation between int and float");
        }

        if (left.GetType() == typeof(double) && right.GetType() == typeof(int))
        {
            throw new ExecutionException("Cannot do math operation between float and int");
        }

        if (left.GetType() == typeof(int) && right.GetType() == typeof(string))
        {
            throw new ExecutionException("Cannot do math operation between int and string");
        }

        if (left.GetType() == typeof(double) && right.GetType() == typeof(string))
        {
            throw new ExecutionException("Cannot do math operation between float and string");
        }

        if (left.GetType() == typeof(string) && right.GetType() == typeof(int))
        {
            throw new ExecutionException("Cannot do math operation between string and int");
        }

        if (left.GetType() == typeof(string) && right.GetType() == typeof(float))
        {
            throw new ExecutionException("Cannot do math operation between string and float");
        }
    }
}
