using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerInterpret.Parser
{
    public class IfStatement: Statement
    {
        public Condition Condition { get; set; }
        public List<Statement> IfStatements { get; set; } //if condition is true
        public List<Statement>? ElseStatements { get; set; } //if condition is false

        public IfStatement(Condition condition, List<Statement> ifStatements)
        {
            Condition = condition;
            IfStatements = ifStatements;
        }

        public IfStatement(Condition condition, List<Statement> ifStatements, List<Statement>? elseStatements) : this(condition, ifStatements)
        {
            ElseStatements = elseStatements;
        }

        override
        public string? ToString()
        {
            string? result = "";
            result += "\n\t\t\t" + "[if statements] ";
            if (IfStatements != null)
            {
                foreach (var argument in IfStatements)
                {
                    result += "\n\t\t\t\t" + argument.ToString();
                }
            }

            result += "\n\t\t\t" + "[else statements] ";
            if (ElseStatements != null)
            {
                foreach (var statement in ElseStatements)
                {
                    result += "\n\t\t\t\t" + statement.ToString();
                }
            }
            return "[if condition: ] " + Condition + result;
        }
    }
}
