using TypeScriptInterpreter.Context;
using TypeScriptInterpreter.Tokenizer;

namespace TypeScriptInterpreter.LangParser.Conditions;

public class BinaryCondition : Condition
{
    public Token OperatorVar { get; }
    public Evaluatable Right { get; }
    public Evaluatable Left { get; }

    public BinaryCondition(Evaluatable left, Token operatorVar, Evaluatable right)
    {
        Left = left;
        OperatorVar = operatorVar;
        Right = right;
    }

    public override bool Evaluate(InterpreterExecutionContext context)
    {
        object left = Left.Evaluate(context);
        object right = Right.Evaluate(context);

        switch (OperatorVar.Type)
        {
            case TokenType.LESS: return ResolveLess(left, right);
            case TokenType.GREATER: return ResolveGreater(left, right);
            case TokenType.LESS_EQUALS: return ResolveLessEqual(left, right);
            case TokenType.GREATER_EQUALS: return ResolveGreaterEqual(left, right);
            case TokenType.DOUBLE_EQUALS: return left.Equals(right);
            default: throw new ParserException("Wrong token type.");
        }
    }

    private bool ResolveLess(object left, object right)
    {
        CheckDifferentTypes(left, right);

        if (left.GetType() == typeof(int) && right.GetType() == typeof(int))
        {
            return (int)left < (int)right;
        }

        return (double)left < (double)right;
    }
    private bool ResolveGreater(object left, object right)
    {
        CheckDifferentTypes(left, right);

        if (left.GetType() == typeof(int) && right.GetType() == typeof(int))
        {
            return (int)left > (int)right;
        }

        return (double)left > (double)right;
    }

    private bool ResolveLessEqual(object left, object right)
    {
        CheckDifferentTypes(left, right);

        if (left.GetType() == typeof(int) && right.GetType() == typeof(int))
        {
            return (int)left <= (int)right;
        }

        return (double)left <= (double)right;
    }

    private bool ResolveGreaterEqual(object left, object right)
    {
        CheckDifferentTypes(left, right);

        if (left.GetType() == typeof(int) && right.GetType() == typeof(int))
        {
            return (int)left >= (int)right;
        }

        return (double)left >= (double)right;
    }

    private void CheckDifferentTypes(object left, object right)
    {
        if (left.GetType() == typeof(int) && right.GetType() == typeof(double))
        {
            throw new ExecutionException("Cannot do comparison operation between int and float");
        }

        if (left.GetType() == typeof(double) && right.GetType() == typeof(int))
        {
            throw new ExecutionException("Cannot do comparison operation between float and int");
        }

        if (left.GetType() == typeof(int) && right.GetType() == typeof(string))
        {
            throw new ExecutionException("Cannot do comparison operation between int and string");
        }

        if (left.GetType() == typeof(double) && right.GetType() == typeof(string))
        {
            throw new ExecutionException("Cannot do comparison operation between float and string");
        }

        if (left.GetType() == typeof(string) && right.GetType() == typeof(int))
        {
            throw new ExecutionException("Cannot do comparison operation between string and int");
        }

        if (left.GetType() == typeof(string) && right.GetType() == typeof(double))
        {
            throw new ExecutionException("Cannot do comparison operation between string and float");
        }
    }
}