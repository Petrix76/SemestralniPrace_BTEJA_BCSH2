using Interpreter.Context;
using Interpreter.LangParser.Conditions;
using Interpreter.LangParser.Expressions;
using Interpreter.LangParser.Statements;
using Interpreter.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Interpreter.LangParser;

public class Parser
{
    public List<Token> Tokens { get; private set; }
    public int Index { get; private set; } = 0;
    public Block ProgramBlock { get; private set; }

    public Parser(List<Token> tokens)
    {
        Tokens = tokens;
    }

    public Block Parse()
    {
        ProgramBlock = ReadBlock();
        return ProgramBlock;
    }

    private Block ReadBlock()
    {
        List<Statement> statements = new List<Statement>();
        List<Function> functions = new List<Function>();

        while (Index != Tokens.Count)
        {
            if (Match(TokenType.FUNCTION))
            {
                functions.Add(ReadFunctionDeclaration());
                continue;
            }
            statements.Add(ReadStatement());
        }

        Block block = new Block(statements, functions);

        return block;
    }

    private Block ReadFunctionBody()
    {
        List<Statement> statements = new List<Statement>();
        List<Function> functions = new List<Function>();

        while (!Match(TokenType.RIGHT_BRACE))
        {
            if (Match(TokenType.FUNCTION))
            {
                functions.Add(ReadFunctionDeclaration());
                continue;
            }
            statements.Add(ReadStatement());
        }

        Block block = new Block(statements, functions);

        return block;
    }

    private Statement ReadStatement()
    {
        switch (PeekToken().Type)
        {
            case TokenType.LET: return ReadLetStatement();
            case TokenType.IDENT: return ReadSetStatement();
            case TokenType.IF: return ReadIfStatement();
            case TokenType.WHILE: return ReadWhileStatement();
            case TokenType.RETURN: return ReadReturnStatement();
            case TokenType.CONTINUE: return ReadContinueStatement();
            case TokenType.BREAK: return ReadBreakStatement();
            default: throw new ParserException("Wrong token type.");
        }
    }

    private Statement ReadBreakStatement()
    {
        CheckTokenType(TokenType.BREAK);
        CheckTokenType(TokenType.SEMI_COLON);

        return new BreakStatement();
    }

    private Statement ReadContinueStatement()
    {
        CheckTokenType(TokenType.CONTINUE);
        CheckTokenType(TokenType.SEMI_COLON);

        return new ContinueStatement();
    }

    private Statement ReadReturnStatement()
    {
        CheckTokenType(TokenType.RETURN);
        if (Match(TokenType.SEMI_COLON))
        {
            CheckTokenType(TokenType.SEMI_COLON);
            return new ReturnStatement(null);
        }

        Expression expression = ReadExpression();
        CheckTokenType(TokenType.SEMI_COLON);

        return new ReturnStatement(expression);
    }

    private Function ReadFunctionDeclaration()
    {
        List<FunctionParameter> parameters = new List<FunctionParameter>();

        CheckTokenType(TokenType.FUNCTION);
        Token identToken = CheckTokenType(TokenType.IDENT);
        
        // parameter parsing
        CheckLeftParen();
        Token paramIdentToken;
        Token typeToken;
        if (Match(TokenType.IDENT))
        {
            paramIdentToken = CheckTokenType(TokenType.IDENT);
            CheckTokenType(TokenType.DOUBLE_DOT);
            typeToken = CheckTypeToken();
            parameters.Add(new FunctionParameter(paramIdentToken.Lexeme, (VarType)Enum.Parse(typeof(VarType), typeToken.Lexeme.ToUpper())));
        }
        while (Match(TokenType.COMMA))
        {
            CheckTokenType(TokenType.COMMA);
            paramIdentToken = CheckTokenType(TokenType.IDENT);
            CheckTokenType(TokenType.DOUBLE_DOT);
            typeToken = CheckTypeToken();

            parameters.Add(new FunctionParameter(paramIdentToken.Lexeme, (VarType)Enum.Parse(typeof(VarType), typeToken.Lexeme.ToUpper())));
        }
        CheckRightParen();

        CheckTokenType(TokenType.DOUBLE_DOT);
        Token functionTypeToken = CheckFunctionTypeToken();

        //parsing body
        CheckLeftBrace();
        Block block = ReadFunctionBody();
        CheckRightBrace();

        return new Function(identToken.Lexeme, block, parameters, (FunctionReturnType) Enum.Parse(typeof(FunctionReturnType), functionTypeToken.Lexeme.ToUpper()));
    }

    private Statement ReadLetStatement()
    {
        CheckTokenType(TokenType.LET);
        Token identToken = CheckTokenType(TokenType.IDENT);
        CheckTokenType(TokenType.DOUBLE_DOT);
        Token typeToken = CheckTypeToken();
        CheckTokenType(TokenType.ASSIGN);
        Expression expr = ReadExpression();
        CheckTokenType(TokenType.SEMI_COLON);

        return new LetStatement(identToken.Lexeme, (VarType)Enum.Parse(typeof(VarType), typeToken.Lexeme.ToUpper()), expr);
    }

    private Statement ReadSetStatement()
    {
        Token identToken = CheckTokenType(TokenType.IDENT);
        Expression expression;
        if (Match(TokenType.LEFT_PAREN))
        {
            expression = ReadFunctionCall(identToken);
            CheckTokenType(TokenType.SEMI_COLON);

            return new CallStatement(identToken.Lexeme, expression);
        }

        CheckAssign();
        expression = ReadExpression();
        CheckTokenType(TokenType.SEMI_COLON);

        return new SetStatement(identToken, expression);
    }

    private Statement ReadIfStatement()
    {
        CheckTokenType(TokenType.IF);
        CheckLeftParen();
        Condition condition = ReadCondition();
        CheckRightParen();

        List<Statement> statements = new List<Statement>();
        List<Statement> elseIfStatements = new List<Statement>();
        Statement? elseStatement = null;

        CheckLeftBrace();
        while (!Match(TokenType.RIGHT_BRACE)) statements.Add(ReadStatement());
        CheckRightBrace();

        while (Match(TokenType.ELSE))
        {
            CheckTokenType(TokenType.ELSE);

            if (Match(TokenType.IF))
            {
                elseIfStatements.Add(ReadElseIfStatement());
                continue;
            }

            elseStatement = ReadElseStatement();
        }

        return new IfStatement(condition, statements, elseIfStatements, elseStatement);
    }

    private Statement ReadElseStatement()
    {
        List<Statement> statements = new List<Statement>();
        CheckLeftBrace();
        while (!Match(TokenType.RIGHT_BRACE)) statements.Add(ReadStatement());
        CheckRightBrace();

        return new ElseStatement(statements);
    }

    private Statement ReadElseIfStatement()
    {
        CheckTokenType(TokenType.IF);
        CheckLeftParen();
        Condition condition = ReadCondition();
        CheckRightParen();

        List<Statement> statements = new List<Statement>();
        CheckLeftBrace();
        while (!Match(TokenType.RIGHT_BRACE)) statements.Add(ReadStatement());
        CheckRightBrace();

        return new ElseIfStatement(condition, statements);
    }

    private Statement ReadWhileStatement()
    {
        CheckTokenType(TokenType.WHILE);
        CheckLeftParen();
        Condition condition = ReadCondition();
        CheckRightParen();

        List<Statement> statements = new List<Statement>(); 

        CheckLeftBrace();
        while (!Match(TokenType.RIGHT_BRACE)) statements.Add(ReadStatement());
        CheckRightBrace();

        return new WhileStatement(condition, statements);
    }
    private Condition ReadCondition()
    {
        Evaluatable expr = ReadExpression();

        if (Match(TokenType.ASSIGN, TokenType.GREATER, TokenType.GREATER_EQUALS, TokenType.LESS, TokenType.LESS_EQUALS))
        {
            Token operatorVar = ReadToken();
            if (Match(TokenType.ASSIGN))
            {
                ReadToken();
                operatorVar = new Token(TokenType.DOUBLE_EQUALS, "==", null, operatorVar.Line);
            }
            Expression right = ReadExpression();

            return new BinaryCondition(expr, operatorVar, right);
        }

        return new ExpressionCondition(expr);
    }

    private Expression ReadExpression()
    {
        Expression expr = ReadBinaryExpression();

        while (Match(TokenType.MINUS, TokenType.PLUS))
        {
            Token operatorVar = ReadToken();
            Expression right = ReadBinaryExpression();
            expr = new BinaryExpression(expr, operatorVar, right);
        }

        return expr;
    }

    private Expression ReadBinaryExpression()
    {
        Expression expr = ReadUnaryExpression();

        while (Match(TokenType.DIVIDE, TokenType.STAR))
        {
            Token operatorVar = ReadToken();
            Expression right = ReadUnaryExpression();
            expr = new BinaryExpression(expr, operatorVar, right);
        }

        return expr;
    }

    private Expression ReadUnaryExpression()
    {
        if (Match(TokenType.MINUS))
        {
            Token operatorVar = ReadToken();
            Expression right = ReadUnaryExpression();
            return new UnaryExpression(operatorVar, right);
        }

        return ReadLiteralExpression();
    }

    private Expression ReadLiteralExpression()
    {
        if (Match(TokenType.NUMBER))
        {
            return new LiteralExpression(ReadToken().Literal, TokenType.NUMBER);
        }

        if (Match(TokenType.STRING))
        {
            return new LiteralExpression(ReadToken().Literal, TokenType.STRING);
        }

        if (Match(TokenType.IDENT))
        {
            Token identToken = CheckTokenType(TokenType.IDENT);

            if (!Match(TokenType.LEFT_PAREN))
            {
                return new IdentExpression(identToken);
            }

            return ReadFunctionCall(identToken);
        }

        if (Match(TokenType.LEFT_PAREN))
        {
            CheckLeftParen();
            Expression expression = ReadExpression();
            CheckRightParen();
            return new GroupExpression(expression);
        }

        throw new ParserException("Expression syntax error.");
    }

    private Expression ReadFunctionCall(Token identToken)
    {
        List<Expression> parameters = new List<Expression>();

        CheckLeftParen();
        if (Match(TokenType.STRING) || Match(TokenType.NUMBER) || Match(TokenType.IDENT))
        {
            parameters.Add(ReadExpression());
        }

        while (Match(TokenType.COMMA))
        {
            CheckTokenType(TokenType.COMMA);
            parameters.Add(ReadExpression());
        }
        CheckRightParen();

        return new CallExpression(identToken.Lexeme, parameters);
    }

    private bool Match(params TokenType[] types)
    {
        if (Index == Tokens.Count) return false;
        return types.Contains(PeekToken().Type);
    }

    private Token PeekToken()
    {
        return Tokens[Index];
    }

    private Token ReadToken()
    {
        return Tokens[Index++];
    }

    private void CheckRightBrace()
    {
        CheckTokenType(TokenType.RIGHT_BRACE);
    }

    private void CheckLeftBrace()
    {
        CheckTokenType(TokenType.LEFT_BRACE);
    }

    private void CheckRightParen()
    {
        CheckTokenType(TokenType.RIGHT_PAREN);
    }

    private void CheckLeftParen()
    {
        CheckTokenType(TokenType.LEFT_PAREN);
    }

    private void CheckAssign()
    {
        CheckTokenType(TokenType.ASSIGN);    
    }

    private Token CheckTokenType(TokenType tokenType)
    {
        Token token = ReadToken();
        if (token.Type != tokenType) throw new ParserException($"{tokenType} token expected at line {token.Line}");
        return token;
    }

    private Token CheckTypeToken()
    {
        List<TokenType> types = new List<TokenType>() { TokenType.FLOAT_TYPE, TokenType.STRING_TYPE, TokenType.INT_TYPE };
        Token token = ReadToken();
        if (!types.Contains(token.Type))
        {
            throw new ParserException($"({TokenType.INT_TYPE},{TokenType.FLOAT_TYPE},{TokenType.STRING_TYPE}) token expected at line {token.Line}");
        }

        return token;
    }

    private Token CheckFunctionTypeToken()
    {
        List<TokenType> types = new List<TokenType>() { TokenType.FLOAT_TYPE, TokenType.STRING_TYPE, TokenType.INT_TYPE, TokenType.VOID_TYPE };
        Token token = ReadToken();
        if (!types.Contains(token.Type))
        {
            throw new ParserException($"({TokenType.INT_TYPE},{TokenType.FLOAT_TYPE},{TokenType.STRING_TYPE},{TokenType.VOID_TYPE}) token expected at line {token.Line}");
        }

        return token;
    }
}