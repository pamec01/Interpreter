using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerInterpret.Parser
{
    public class Expression
    {

        public Term Term1 { get; set; }
        public String? Token { get; set; }
        public Term? Term2 { get; set; }

        public Expression(Term term1)
        {
            Term1 = term1;
        }

        public Expression(Term term1, string token, Term term2) 
        { 
            if( !(token.Equals("+") || token.Equals("-"))) 
            {
                //throw new Exception("invalid token type: " + token);
            }
            Term1 = term1;
            Token = token;
            Term2 = term2;
        }

        override
        public string? ToString()
        {
            return "[term1] " + Term1.ToString();
        }

    }
}
