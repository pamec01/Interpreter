using CompilerInterpret.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerInterpret.Parser
{
    public class SetStatement: Statement
    {

        public string Identifier { get; set; }
        public Expression? Expression { get; set; }
        public string? Value { get; set; }
        public string? Operator { get; set; }
        public string? Value2 { get; set; }
        public FunctionCallStatement? FuncCall { get; set; }


        public SetStatement(Token identifier, Expression expression)
        {
            Identifier = identifier.Value;
            Expression = expression;
        }

        public SetStatement(Token identifier, string value)
        {
            Identifier = identifier.Value;
            Value = value;
        }

        public SetStatement(string identifier, string value)
        {
            Identifier = identifier;
            Value = value;
        }

        public SetStatement(Token identifier, string value, string op, string value2)
        {
            Identifier = identifier.Value;
            Value = value;
            Operator = op;
            Value2 = value2;
        }

        public SetStatement(Token identifier, FunctionCallStatement funcCall)
        {
            Identifier = identifier.Value;
            FuncCall = funcCall;
        }
    }
}
