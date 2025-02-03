using CompilerInterpret.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerInterpret.Parser
{
    public class VariableTable
    {
        public string Name { get; set; }
        public Token Token { get; set; }
        public string Scope { get; set; }
        public string Value { get; set; }

        public VariableTable(string name, Token token, string scope)
        {
            Name = name;
            Token = token;
            Scope = scope;
        }

        public VariableTable(string name, Token token, string scope, string value)
        {
            Name = name;
            Token = token;
            Scope = scope;
            Value = value;
        }
    }
}
