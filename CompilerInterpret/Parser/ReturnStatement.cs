using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerInterpret.Parser
{
    public class ReturnStatement: Statement
    {

        public Expression? Expression { get; set; }

        public ReturnStatement(Expression? expression)
        {
            Expression = expression;
        }

        override
        public string? ToString()
        {
            if(Expression == null) return null;

            return "[Return Expression] " + Expression;
        }

    }
}
