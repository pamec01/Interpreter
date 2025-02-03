using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerInterpret.Parser
{
    public class WhileStatement: Statement
    {

        public Condition Condition { get; set; }
        public List<Statement> Statements { get; set; }

        public WhileStatement(Condition condition, List<Statement> statements)
        {
            Condition = condition;
            Statements = statements;
        }

    }
}
