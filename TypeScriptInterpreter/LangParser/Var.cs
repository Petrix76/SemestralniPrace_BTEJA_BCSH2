using TypeScriptInterpreter.Tokenizer;

namespace TypeScriptInterpreter.LangParser;

public class Var
{
    public string Ident { get; }

    public VarType Type { get; }

    public object Value { get; set; }

    public Var(string ident, object value, VarType varType)
    {
        Ident = ident;
        Value = value;
        Type = varType;
    }
}
