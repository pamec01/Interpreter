using CompilerInterpret.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerInterpret.Parser
{
    public class VariableStatement : Statement
    {

        public TokenType Type { get; set; }
        public String Ident { get; set; }
        public Expression? Expression { get; set; }
        public FunctionCallStatement? FuncCall { get; set; }

        public VariableStatement(TokenType type, string ident) 
        {
            
            Type = type;
            Ident = ident;
        }

        public VariableStatement(TokenType type, string ident, Expression? expression): this(type, ident)
        {
            Expression = expression;
        }

        public VariableStatement(TokenType type, string ident, FunctionCallStatement funcCall)
        {
            Type = type;
            Ident = ident;
            FuncCall = funcCall;
        }

        override
        public string? ToString()
        {
            string exp;
            string func = "";
            if(Expression == null)
            {
                exp = "";
            }
            else
            {
                exp = Expression.ToString();
            }
            if (FuncCall != null) { func = FuncCall.ToString(); }
            return "[type] " + Type + " [ident] " + Ident + " [Expression] " + exp + " [func call] " + func;
        }
    }
}
