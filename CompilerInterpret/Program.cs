using CompilerInterpret.Interpreter;
using CompilerInterpret.Lexer;
using CompilerInterpret.Parser;
using System;
using System.IO;
using System.Text;

public class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Running...");
        string code = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../CompilerInterpret/Grammar/example2.txt"));

        Lexer lexer = new Lexer();
        List<Token> list = lexer.ScanTokens(code);

        foreach (Token token in list) 
        { 
            Console.WriteLine(token.Type + " value: " + token.Value + " line: " + token.Line);
        }
        Console.WriteLine("=====TOKEN LIST END=====");

        Parser parser = new Parser(list);
        ProgramBlock block = parser.Parse();

        Console.WriteLine(block.Statements.Count());
        for (int i = 0; i < block.Statements.Count(); i++)
        {
            Console.WriteLine(block.Statements[i]);
        }

        Console.WriteLine("===== INTERPRETER =====");
        Interpreter interpreter = new Interpreter(block.Statements, parser.variableTableList);
    }
}