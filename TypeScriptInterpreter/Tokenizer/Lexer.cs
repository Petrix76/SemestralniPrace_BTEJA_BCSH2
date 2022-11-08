using System;
using System.Collections.Generic;

namespace Interpreter.Tokenizer;

public class Lexer
{
    private Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>()
    {
        { "ident", TokenType.IDENT },
        { "number", TokenType.NUMBER },
        { "let", TokenType.LET },
        { "function", TokenType.FUNCTION },
        { "if", TokenType.IF },
        { "else", TokenType.ELSE },
        { "while", TokenType.WHILE },
        { "continue", TokenType.CONTINUE },
        { "break", TokenType.BREAK },
        { "return", TokenType.RETURN },
        { "string", TokenType.STRING_TYPE },
        { "int", TokenType.INT_TYPE },
        { "float", TokenType.FLOAT_TYPE },
        { "void", TokenType.VOID_TYPE }
    };

    private string source;
    private List<Token> tokens = new List<Token>();
    private int index = 0;

    private int start = 0;
    private int current = 0;
    private int line = 1;

    public Lexer(string source)
    {
        this.source = source;
    }

    public List<Token> GetTokens()
    {
        while(!IsAtEnd())
        {
            start = current;
            GetToken();
        }

        return tokens;
    }

    private void GetToken()
    {
        char c = Advance();
        switch (c)
        {
            case '(': AddToken(TokenType.LEFT_PAREN); break;
            case ')': AddToken(TokenType.RIGHT_PAREN); break;
            case '{': AddToken(TokenType.LEFT_BRACE); break;
            case '}': AddToken(TokenType.RIGHT_BRACE); break;
            case ',': AddToken(TokenType.COMMA); break;
            case '-': AddToken(TokenType.MINUS); break;
            case '+': AddToken(TokenType.PLUS); break;
            case ';': AddToken(TokenType.SEMI_COLON); break;
            case '*': AddToken(TokenType.STAR); break;
            case '/': AddToken(TokenType.DIVIDE); break;
            case '=': AddToken(TokenType.ASSIGN); break;
            case '"': TokenizeString(); break;
            case ':': AddToken(TokenType.DOUBLE_DOT); break;
            case '<': AddToken(Match('=') ? TokenType.LESS_EQUALS : TokenType.LESS); break;
            case '>': AddToken(Match('=') ? TokenType.GREATER_EQUALS : TokenType.GREATER); break;
            case ' ':
            case '\r':
            case '\t':
                break;
            case '\n':
                line++;
                break;
            default:
                if (IsDigit(c))
                {
                    Number();
                }
                else if (IsAlpha(c))
                {
                    Identifier();
                }
                break;
        }
    }

    private bool IsAtEnd()
    {
        return current >= source.Length;
    }

    private char Advance()
    {
        return source[current++];
    }

    private void AddToken(TokenType type)
    {
        addToken(type, null);
    }

    private void addToken(TokenType type, object literal)
    {
        string text = source.Substring(start, current - start);
        tokens.Add(new Token(type, text, literal, line));
    }

    private bool Match(char expected)
    {
        if (IsAtEnd()) return false;
        if (source[current] != expected) return false;

        current++;
        return true;
    }

    private void TokenizeString()
    {
        while (Peek() != '"' &&  !IsAtEnd()) {
            if (Peek() == '\n') line++;
            Advance();
        }

            if (IsAtEnd()) {
                throw new Exception("Unterminated string.");
            }

            Advance();

            string value = source.Substring(start + 1, current - start - 1);
            addToken(TokenType.STRING, value);
    }

    private bool IsAlpha(char c)
    {
        return (c >= 'a' && c <= 'z') ||
               (c >= 'A' && c <= 'Z') ||
                c == '_';
    }

    private bool IsAlphaNumeric(char c)
    {
        return IsAlpha(c) || IsDigit(c);
    }

    private bool IsDigit(char c)
    {
        return c >= '0' && c <= '9';
    }

    private void Number()
    {
        while (IsDigit(Peek())) Advance();

        if (Peek() == '.' && IsDigit(PeekNext()))
        {
            Advance();

            while (IsDigit(Peek())) Advance();
        }

        addToken(TokenType.NUMBER, double.Parse(source.Substring(start, current - start)));
    }

    private char Peek()
    {
        if (IsAtEnd()) return '\0';
        return source[current];
    }

    private char PeekNext()
    {
        if (current + 1 >= source.Length) return '\0';
        return source[current + 1];
    }

    private void Identifier()
    {
        while (IsAlphaNumeric(Peek())) Advance();

        string text = source.Substring(start, current - start);
        TokenType type;
        keywords.TryGetValue(text.ToLower(), out type);

        if (type == null) type = TokenType.IDENT;
        AddToken(type);
    }
}
