using CompilerInterpret.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerInterpret.Parser
{
    public class Argument
    {

        public string Identifier { get; set; }
        public TokenType Type { get; set; }
        public string Value { get; set; }

        public Argument(Token token, Token identifier)
        {
            Identifier = identifier.Value;
            Type = token.Type;
        }

        public Argument(string value)
        {
            Value = value;
        }

        override
        public string? ToString()
        {
            return "type: " + Type + " ident: " + Identifier + " value: " + Value;
            
        }

    }
}
