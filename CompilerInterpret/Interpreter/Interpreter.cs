using CompilerInterpret.Lexer;
using CompilerInterpret.Parser;

namespace CompilerInterpret.Interpreter
{
    public class Interpreter
    {
        private readonly Dictionary<string, FunctionDeclarationStatement> _functions = new();
        private readonly Stack<string> _scopeStack = new(); // Stack to track current scope
        public List<VariableTable> _variableTable;

        public Interpreter(List<Statement> statements, List<VariableTable> table)
        {
            _variableTable = table;
            _scopeStack.Push("global"); // Start with global scope
            RegisterFunctions(statements); // Register all functions
            RegisterBuiltInFunctions();

            foreach (Statement statement in statements)
            {
                ExecuteStatement(statement);
            }
            ExecuteFunction("main", new List<object>());
        }

        private void RegisterFunctions(List<Statement> statements)
        {
            foreach (var statement in statements)
            {
                if (statement is FunctionDeclarationStatement function)
                {
                    if (_functions.ContainsKey(function.Identifier))
                    {
                        throw new Exception($"Function {function.Identifier} is already defined.");
                    }
                    _functions[function.Identifier] = function;
                }
            }

            // Ensure main function exists
            if (!_functions.ContainsKey("main"))
            {
                throw new Exception("No main function defined.");
            }
        }

        private void RegisterBuiltInFunctions()
        {
            var arg = new List<Argument>();
            arg.Add(new Argument("arg"));
            _functions["print"] = new FunctionDeclarationStatement("print", arg, null, new List<Statement>());
            _functions["println"] = new FunctionDeclarationStatement("println", arg, null, new List<Statement>());
        }

        public void ExecuteStatement(Statement statement)
        {
            switch (statement)
            {
                case FunctionDeclarationStatement function:
                    break;

                case IfStatement ifStatement:
                    ExecuteIfStatement(ifStatement);
                    break;

                case WhileStatement whileStatement:
                    ExecuteWhileStatement(whileStatement);
                    break;

                case FunctionCallStatement functionCall:
                    ExecuteFunctionCall(functionCall);
                    break;

                case SetStatement setStatement:
                    ExecuteSetStatement(setStatement);
                    break;
            }
        }

        private void ExecuteWhileStatement(WhileStatement whileStatement)
        {
            
            int cond = Int32.Parse(GetVariable(whileStatement.Condition.Token1.Value).Value);
            while (Int32.Parse(GetVariable(whileStatement.Condition.Token1.Value).Value) < int.Parse(whileStatement.Condition.Token3.Value)) 
            {
                foreach (Statement st in whileStatement.Statements)
                {
                    ExecuteStatement(st);
                }
            }
        }

        private void ExecuteSetStatement(SetStatement setStatement)
        {
            if (setStatement.Value != null && setStatement.Value2 == null) // a = 1;
            {
                VariableTable var = GetVariable(setStatement.Identifier);
                var.Value = setStatement.Value;
                SetVariable(setStatement.Identifier, var);
            } else if (setStatement.Value2 != null)     // a = a + 1;
            {
                VariableTable var = GetVariable(setStatement.Identifier);
                var.Value = (Int32.Parse(GetVariable(setStatement.Value).Value) + Int32.Parse(setStatement.Value2)).ToString();

                SetVariable(setStatement.Identifier, var);
            }
            else if (setStatement.FuncCall != null)     // a = func(a);
            {
                // func call result
                var functionResult = ExecuteFunction(setStatement.FuncCall.Identifier,
                    setStatement.FuncCall.Arguments.Select(arg => EvaluateExpression(arg)).ToList());

                // add result to var table
                VariableTable var = GetVariable(setStatement.Identifier);
                var.Value = functionResult.ToString();
                SetVariable(setStatement.Identifier, var);
            }
        }

        public object ExecuteFunction(string functionName, List<object> arguments)
        {
            if (!_functions.TryGetValue(functionName, out var function))
            {
                throw new Exception($"Function {functionName} not defined.");
            }

            var scopeName = functionName;
            _scopeStack.Push(scopeName);

            for (int i = 0; i < arguments.Count; i++)
            {
                string variableName = $"{scopeName}.{function.Arguments[i].Identifier}";
                //SetVariable(variableName, new VariableTable(variableName, new Token(arguments[i].ToString()), scopeName));
            }

            object returnValue = null;
            foreach (var stmt in function.Statements)
            {
                if (stmt is ReturnStatement returnStmt)
                {
                    //returnValue = EvaluateExpression(returnStmt.Expression);
                    if (GetVariable(returnStmt.Expression.Term1.Factor1.Variable) != null)
                    {
                        returnValue = GetVariable(returnStmt.Expression.Term1.Factor1.Variable).Value;
                    }
                    else { returnValue = returnStmt.Expression.Term1.Factor1.Variable; }
                    
                    break;
                }
                ExecuteStatement(stmt);
            }

            _scopeStack.Pop();
            return returnValue;
        }

        private void ExecuteFunctionCall(FunctionCallStatement functionCall)
        {
            if (functionCall.Identifier.Equals("print") || functionCall.Identifier.Equals("println"))
            {
                if (functionCall.Argument.Equals(""))
                {
                    foreach (var arg in functionCall.Arguments)
                    {
                        Console.WriteLine(GetVariable(arg.Term1.Factor1.Variable).Value);
                    }
                } 
                else 
                { Console.WriteLine(functionCall.Argument);  }
                
            }
            else
            {
                var arguments = new List<object>();
                foreach (var arg in functionCall.Arguments)
                {
                    arguments.Add(EvaluateExpression(arg));
                }

                ExecuteFunction(functionCall.Identifier, arguments);
            }
        }

        private object EvaluateExpression(Expression expression)
        {
            return null;
        }

        private void ExecuteIfStatement(IfStatement ifStatement)
        {
            if (EvaluateCondition(ifStatement.Condition))
            {
                foreach (var stmt in ifStatement.IfStatements)
                {
                    ExecuteStatement(stmt);
                }
            }
            else if (ifStatement.ElseStatements != null)
            {
                foreach (var stmt in ifStatement.ElseStatements)
                {
                    ExecuteStatement(stmt);
                }
            }
        }

        private bool EvaluateCondition(Condition condition)
        {
            if (condition.SingleExp != null)
            {
                return Convert.ToBoolean(condition.SingleExp);
            }
            else 
            {
                if (GetVariable(condition.Token1.Value) != null)
                {
                    return int.Parse(GetVariable(condition.Token1.Value).Value) > int.Parse(condition.Token3.Value);
                }
                return int.Parse(condition.Token1.Value) > int.Parse(condition.Token3.Value);
            }
            
        }

        private void SetVariable(string name, VariableTable variable)
        {
            var existing = _variableTable.FirstOrDefault(v => v.Name == name);
            if (existing != null)
            {
                existing.Token = variable.Token;
            }
            else
            {
                _variableTable.Add(variable);
            }
        }

        private VariableTable GetVariable(string name)
        {
            return _variableTable.FirstOrDefault(v => v.Name == name)
                ?? null;
        }
    }
}
