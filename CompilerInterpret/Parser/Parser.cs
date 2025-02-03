using CompilerInterpret.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerInterpret.Parser
{
    public class Parser
    {

        private List<Token> tokens;
        public List<VariableTable> variableTableList = new List<VariableTable>();
        private string scope = "";

        public Parser(List<Token> lexerTokens)
        {
            tokens = lexerTokens;
        }

        public ProgramBlock Parse() 
        {

            List<Statement> statements = new List<Statement>();

            while (tokens.Count != 0) 
            {
                statements.Add(ReadStatement());
            }

            ProgramBlock program = new ProgramBlock(statements);
            Console.WriteLine("=====variable table:=====");
            foreach (VariableTable table in variableTableList) {
                Console.WriteLine("[name] " + table.Name + " [type] " + table.Token.Type + " [value] " + table.Value + " [scope] " + table.Scope);
            }
            Console.WriteLine("=====end variable table:=====");
            return program;
        }

        public Statement ReadStatement()
        {
            // ARRAY
            if (tokens[1].Type.Equals(TokenType.Left_Square_Bracket) || tokens[2].Type.Equals(TokenType.Left_Square_Bracket))
            {
                Console.WriteLine("Reading array cycle");
                return ReadArrayStatement();
            }
            else
            // FUNCTION DECLARATION
            if (tokens.Count > 3 && tokens[2].Type.Equals(TokenType.Left_Bracket))
            {
                Console.WriteLine("Reading function declaration");
                return ReadFunctionDeclarationStatement();
            }
            else
            //IF STATEMENT
            if (tokens[0].Type.Equals(TokenType.If))
            {
                Console.WriteLine("Reading if statement");
                return ReadIfStatement();
            }
            else
            //WHILE CYCLE
            if (tokens[0].Type.Equals(TokenType.While))
            {
                Console.WriteLine("Reading while cycle");
                return ReadWhileStatement();
            }
            else
            
            // FUNCTION CALL
            if (tokens.Count > 2 && tokens[1].Type.Equals(TokenType.Left_Bracket) && !(tokens[0].Equals(TokenType.If)))
            {
                Console.WriteLine("Reading function call");
                return ReadFunctionCallStatement();
            }
            else
            //RETURN STATEMENT
            if (tokens[0].Type.Equals(TokenType.Return))
            {
                Console.WriteLine("Reading return statement");
                return ReadReturnStatement();
            }
            
            else
            //VARIABLE DECLARATION
            if (tokens.Count > 3 && tokens[2].Type.Equals(TokenType.Equal) || tokens[2].Type.Equals(TokenType.Semi_Colon))
            {
                Console.WriteLine("Reading variable declaration");
                return ReadVariableDeclarationStatement();
            }
            else
            //VARIABLE SET
            if (tokens[1].Type.Equals(TokenType.Equal))
            {
                Console.WriteLine("Reading set variable");
                return ReadSetVariableStatement();
            }
            else
             
            {
                Console.WriteLine("ReadStatement Error on line " + tokens[0].Line + " token: " + tokens[0].Type);
                throw new Exception();
            }
            

            return null;
        }

        private Statement ReadArrayStatement()
        {
            if (tokens[1].Type.Equals(TokenType.Left_Square_Bracket))
            {
                string id = tokens[0].Value + "[" + tokens[2].Value + "]";
                string value = tokens[5].Value;
                RemoveFromList(7);
                return new SetStatement(id, value);
            }
            else
            {
                if (tokens[5].Type.Equals(TokenType.Left_Square_Bracket))   //2d
                {
                    int arrayNum = Int32.Parse(tokens[3].Value);
                    int arrayWidth = Int32.Parse(tokens[6].Value);

                    Token token = tokens[0];
                    string identifier = tokens[1].Value;
                    for (int i = 0; i < arrayNum; i++)
                    {
                        for (int j = 0; j < arrayWidth; j++) {
                            variableTableList.Add(new VariableTable(tokens[1].Value + "[" + i + "]" + "[" + j + "]", token, scope, "0"));
                        }
                        
                    }
                    RemoveFromList(9);

                    return new Statement();
                }
                else
                {
                    int arrayNum = Int32.Parse(tokens[3].Value);

                    Token token = tokens[0];
                    string identifier = tokens[1].Value;
                    for (int i = 0; i < arrayNum; i++)
                    {
                        variableTableList.Add(new VariableTable(tokens[1].Value + "[" + i + "]", token, scope, "0"));
                    }
                    RemoveFromList(6);

                    return new Statement();
                }
            }
        }

        private SetStatement ReadSetVariableStatement()
        {
            if (tokens[3].Type.Equals(TokenType.Semi_Colon))    // a = 4;
            {
                var identifier = tokens[0];
                var value = tokens[2].Value;
                RemoveFromList(4);
                return new SetStatement(identifier, value);
            }
            else if (tokens[5].Type.Equals(TokenType.Semi_Colon)) // a = a + 4;
            {
                var identifier = tokens[0];
                var value = tokens[2].Value;
                var op = tokens[3].Value;
                var val2 = tokens[4].Value;
                RemoveFromList(6);
                return new SetStatement(identifier, value, op, val2);
            }
            //func call
            var id = tokens[0];
            RemoveFromList(2);
            return new SetStatement(id, ReadFunctionCallStatement());

            return null;
        }

        private FunctionCallStatement ReadFunctionCallStatement()
        {
            var ident = tokens[0];

            // Handle string argument case
            if (tokens[2].Type.Equals(TokenType.String_Value))
            {
                var stringValue = tokens[2].Value;
                RemoveFromList(5);  // Remove function name + ( + "hello" + ) + ;
                return new FunctionCallStatement(ident, stringValue);
            }
            else
            {
                // Handle multiple arguments case
                List<Expression> arguments = new List<Expression>();

                RemoveFromList(2); // Remove function name and "("

                // Parse arguments until closing parenthesis
                while (!tokens[0].Type.Equals(TokenType.Right_Bracket))
                {
                    arguments.Add(new Expression(new Term(new Factor(tokens[0].Value))));
                    RemoveFromList(1); // Remove processed argument tokens

                    // If next token is a comma, skip it and continue
                    if (tokens[0].Type.Equals(TokenType.Comma))
                    {
                        RemoveFromList(1);
                    }
                }

                RemoveFromList(2); // Remove ")" and ";"

                return new FunctionCallStatement(ident, arguments);
            }
        }


        private ReturnStatement ReadReturnStatement()
        {
            var returnValue = tokens[1];
            RemoveFromList(3); // removes Return + "value"
            return new ReturnStatement(new Expression(new Term(new Factor(returnValue.Value))));
        }

        private IfStatement ReadIfStatement()
        {
            // Validate and remove "if" and "(" tokens
            if (tokens[0].Type.Equals(TokenType.If) && tokens[1].Type.Equals(TokenType.Left_Bracket))
            {
                RemoveFromList(2);
            }
            else
            {
                throw new Exception("if statement syntax error");
            }
            Console.WriteLine("here");
            
            // Manually parse the condition
            Token leftOperand = tokens[0];
            Token operatorToken = tokens[1];
            Token rightOperand = tokens[2];
            RemoveFromList(3); // Remove condition tokens
            
            Condition condition = new Condition(leftOperand, operatorToken, rightOperand);

            // Validate and remove ")" and "{" tokens
            if (tokens[0].Type.Equals(TokenType.Right_Bracket) && tokens[1].Type.Equals(TokenType.Left_Curly_Bracket))
            {
                RemoveFromList(2);
            }
            else
            {
                throw new Exception("if statement syntax error");
            }

            // Read statements for the "if" block
            List<Statement> ifStatements = new List<Statement>();
            while (!tokens[0].Type.Equals(TokenType.Right_Curly_Bracket))
            {
                ifStatements.Add(ReadStatement());
            }
            RemoveFromList(1); // Remove "}"

            // Check for "else" block
            List<Statement>? elseStatements = null;
            if (tokens[0].Type.Equals(TokenType.Else))
            {
                RemoveFromList(1); // Remove "else"

                if (tokens[0].Type.Equals(TokenType.Left_Curly_Bracket))
                {
                    RemoveFromList(1); // Remove "{"
                    elseStatements = new List<Statement>();

                    // Read statements for the "else" block
                    while (!tokens[0].Type.Equals(TokenType.Right_Curly_Bracket))
                    {
                        elseStatements.Add(ReadStatement());
                    }
                    RemoveFromList(1); // Remove "}"
                }
                else
                {
                    throw new Exception("else statement syntax error");
                }
            }

            // Return the fully constructed IfStatement
            return new IfStatement(condition, ifStatements);
        }


        private WhileStatement ReadWhileStatement()
        {
            if (tokens[0].Type.Equals(TokenType.While) && tokens[1].Type.Equals(TokenType.Left_Bracket))
            {
                RemoveFromList(2);
            }
            else
            {
                throw new Exception("while statement syntax error");
            }

            // Manually parse the condition
            Token leftOperand = tokens[0];
            Token operatorToken = tokens[1];
            Token rightOperand = tokens[2];
            RemoveFromList(3); // Remove condition tokens

            Condition condition = new Condition(leftOperand, operatorToken, rightOperand);

            // Validate and remove ")" and "{" tokens
            if (tokens[0].Type.Equals(TokenType.Right_Bracket) && tokens[1].Type.Equals(TokenType.Left_Curly_Bracket))
            {
                RemoveFromList(2);
            }
            else
            {
                throw new Exception("while statement syntax error");
            }

            // Read statements for the "while" block
            List<Statement> whileStatements = new List<Statement>();
            while (!tokens[0].Type.Equals(TokenType.Right_Curly_Bracket))
            {
                whileStatements.Add(ReadStatement());
            }
            RemoveFromList(1); // Remove "}"

            
            return new WhileStatement(condition, whileStatements);
        }

        private Condition ReadCondition()
        {
            Expression expression = ReadExpression();
            if ((tokens[0].Type.Equals(TokenType.Right_Bracket)))
            {
                return new Condition(expression.Term1.Factor1);
            }
            else
            {
                Token token = ReadToken();
                Expression expression2 = ReadExpression();
                return new Condition(expression, token, expression2);
            }
        }

        private Token ReadToken()
        {
            if (tokens.Count > 0) 
            {
                Token token = tokens[0];
                RemoveFromList(1);
                return token;
            }
            throw new Exception("ReadToken ex no items in list");
        }

        private VariableStatement ReadVariableDeclarationStatement()
        {            
            Token token = tokens[0];
            string identifier = tokens[1].Value;
            
            
            
            if (tokens[2].Type.Equals(TokenType.Semi_Colon))    // int a;
            {
                // no asociation
                if (scope.Equals(""))
                {
                    variableTableList.Add(new VariableTable(identifier, token, scope));
                }
                else
                {
                    variableTableList.Add(new VariableTable(scope + "." + identifier, token, scope));
                }
                RemoveFromList(3);
                return new VariableStatement(token.Type, identifier);
            }
            else    // int a = 5;
            {
                // asociation
                RemoveFromList(3);
                if (tokens[1].Type.Equals(TokenType.Semi_Colon)) //simple asociation
                {
                    if (scope.Equals(""))
                    {
                        variableTableList.Add(new VariableTable(identifier, token, scope, tokens[0].Value));
                    }
                    else
                    {
                        variableTableList.Add(new VariableTable(scope + "." + identifier, token, scope, tokens[0].Value));
                    }
                    return new VariableStatement(token.Type, identifier, ReadExpression());
                }
                else    //function call, set value in interpreter
                {
                    if (scope.Equals(""))
                    {
                        variableTableList.Add(new VariableTable(identifier, token, scope));
                    }
                    else
                    {
                        variableTableList.Add(new VariableTable(scope + "." + identifier, token, scope));
                    }



                    return new VariableStatement(token.Type, identifier, ReadFunctionCallStatement());
                }
                
            }
        }

        private Expression ReadExpression()
        {
            if (tokens[1].Type.Equals(TokenType.Semi_Colon)); // simple asociation ( = 10; )
            {
                var token = tokens[0].Value;
                RemoveFromList(2);
                return new Expression(new Term(new Factor(token)));
            }
            
            throw new NotImplementedException();
        }

        private FunctionDeclarationStatement ReadFunctionDeclarationStatement()
        {
            Token returnType = tokens[0];
            Token ident = tokens[1];
            scope = ident.Value; //save to variable table

            RemoveFromList(3);
            List<Argument> arguments = new List<Argument>();

            // Parse arguments until closing parenthesis is found
            while (!tokens[0].Type.Equals(TokenType.Right_Bracket))
            {
                // Read argument type and name
                Token argType = tokens[0];
                Token argName = tokens[1];
                arguments.Add(new Argument(argType, argName));

                // Remove processed tokens for the argument
                RemoveFromList(2);

                // If next token is a comma ",", skip it and continue; otherwise, break (no more arguments)
                if (tokens[0].Type.Equals(TokenType.Comma))
                {
                    RemoveFromList(1);
                }
            }
            RemoveFromList(2);  //remove ){

            List<Statement> statements = new List<Statement>();
            while (!tokens[0].Type.Equals(TokenType.Right_Curly_Bracket)) 
            {
                statements.Add(ReadStatement());
            }

            RemoveFromList(1); //remove }
            scope = "";
            return new FunctionDeclarationStatement(ident, arguments, returnType, statements);
        }


        private void RemoveFromList(int count) 
        {
            tokens.RemoveRange(0, count);
        }
    }
}
