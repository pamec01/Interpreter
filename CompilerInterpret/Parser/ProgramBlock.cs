using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompilerInterpret.Parser
{
    public class ProgramBlock
    {

        public List<Statement> Statements { get; set; }

        public ProgramBlock()
        {
            Statements = new List<Statement>();
        }

        public ProgramBlock(List<Statement> statements)
        {
            Statements = statements;
        }

        /*public void Evaluate(ExecutionCntxt context)
        {
            foreach (Statement s in Statements)
            {
                s.Evaluate(context);
            }
        }*/
        

    }
}
