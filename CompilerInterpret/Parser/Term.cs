using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerInterpret.Parser
{
    public class Term
    {

        public Factor Factor1 { get; set; }
        public String? Token { get; set; }
        public Factor? Factor2 { get; set; }


        public Term(Factor factor)
        {
            Factor1 = factor;
        }

        public Term(Factor factor1, string token, Factor factor2)
        {
            if (!(token.Equals("+") || token.Equals("-")))
            {
                throw new Exception("invalid token type: " + token);
            }
            Factor1 = factor1;
            Token = token;
            Factor2 = factor2;
        }


        override
        public string? ToString()
        {
            return "[Factor1] " + Factor1 + " [Token] " + Token + " [Factor2] " + Factor2;
        }
    }
}
