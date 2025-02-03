using CompilerInterpret.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerInterpret.Parser
{
    public class FunctionDeclarationStatement: Statement
    {

        public string Identifier { get; set; }
        public List<Argument> Arguments { get; set; }
        public Token ReturnType { get; set; }
        public List<Statement> Statements { get; set; }

        public FunctionDeclarationStatement(Token identifier, List<Argument> arguments, Token returnType, List<Statement> statements)
        {
            Identifier = identifier.Value;
            Arguments = arguments.ToList();
            ReturnType = returnType;
            Statements = statements;
        }

        public FunctionDeclarationStatement(string identifier, List<Argument> arguments, Token returnType, List<Statement> statements)
        {
            Identifier = identifier;
            Arguments = arguments.ToList();
            ReturnType = returnType;
            Statements = statements;
        }


        override
        public string? ToString()
        {
            string ? result = "";
            result += "\n\t" + "[arguments] ";
            if (Arguments != null) 
            { 
                foreach (var argument in Arguments) 
                {
                    result += "\n\t\t" + argument.ToString();
                } 
            }
            
            result += "\n\t" + "[statements] ";
            if (Statements != null)
            {
                foreach (var statement in Statements)
                {
                    try { result += "\n\t\t" + statement.ToString(); }
                    catch (Exception ex) { }
                    
                }
            }
            return "[function id] " + Identifier + result;
        }
    }
}
