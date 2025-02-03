using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerInterpret.Lexer
{
    public class Token
    {

        public TokenType Type { get; set; }
        public string Value { get; set; }
        public int Line { get; set; }

        public Token() { }

        public Token(TokenType type, int line)
        {
            Type = type;
            Line = line;
        }

        public Token(TokenType type, string? value, int line)
        {
            Type = type;
            Value = value;
            Line = line;

        }

        public override string ToString()
        {
            return "token type: " + Type + "value: " + Value;
        }
    }
}
