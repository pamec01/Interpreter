using CompilerInterpret.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CompilerInterpret.Parser
{
    public class Factor
    {

        public int? Number { get; set; }
        public float? RealNumber { get; set; }
        public string? Variable { get; set; }

        /*public Factor(int number) 
        {
            Number = number;
        }
        public Factor(float number) 
        {
            RealNumber = number;
        }*/
        public Factor(string variable) 
        {
            Variable = variable;
        }

        override
        public string? ToString()
        {
            return "[Variable] " + Variable;
        }
    }
}
