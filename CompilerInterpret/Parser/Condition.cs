using CompilerInterpret.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerInterpret.Parser
{
    public class Condition
    {

        public Expression? Expression1 { get; set; }
        public Token? Token1 { get; set; }
        public Token? Token2 { get; set; }
        public Token? Token3 { get; set; }
        public Expression? Expression2 { get; set; }
        public Factor? Factor { get; set; } // if value is true or false
        public string SingleExp { get; set; }

        public Condition(Factor factor)
        {
            Factor = factor;
        }

        public Condition(string singleExp)
        {
            SingleExp = singleExp;
        }

        public Condition(Token token1, Token token2, Token token3)
        {
            Token1 = token1;
            Token2 = token2;
            Token3 = token3;
        }

        public Condition(Expression expression1, Token token, Expression expression2)
        {
            Expression1 = expression1;
            Token1 = token;
            Expression2 = expression2;
        }

    }
}
