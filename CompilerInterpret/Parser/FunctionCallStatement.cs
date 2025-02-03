using CompilerInterpret.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerInterpret.Parser
{
    public class FunctionCallStatement : Statement
    {
        public string Identifier { get; set; }
        public TokenType Type { get; set; }
        //public List<Expression> Arguments { get; set; }
        public List<Expression> Arguments { get; set; }
        public string Argument { get; set; }

        public object? ReturnedValue { get; set; }

        public FunctionCallStatement(Token identifier)
        {
            Identifier = identifier.Value;
            Arguments = new List<Expression>();
            Argument = "";
        }

        public FunctionCallStatement(Token identifier, string argument)
        {
            Identifier = identifier.Value;
            Arguments = new List<Expression>();
            Argument = argument;
        }

        public FunctionCallStatement(Token identifier, List<Expression> arguments)
        {
            Identifier = identifier.Value;
            Arguments = arguments;
            Argument = "";
        }

        override
        public string? ToString()
        {
            if (Arguments.Count > 0)
            {
                var argumentsStr = string.Join(", ", Arguments.Select(arg => arg.ToString()));
                return $"[function call id] {Identifier} [arguments] {argumentsStr}";
            }
            return $"[function call id] {Identifier} [argument] {Argument}";
        }
    }
}
