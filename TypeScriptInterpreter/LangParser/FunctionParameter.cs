namespace TypeScriptInterpreter.LangParser;

public class FunctionParameter
{
    public FunctionParameter(string lexeme, VarType varType)
    {
        Lexeme = lexeme;
        VarType = varType;
    }

    public string Lexeme { get; }
    public VarType VarType { get; }
}
