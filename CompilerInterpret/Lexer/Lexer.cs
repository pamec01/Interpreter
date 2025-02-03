using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerInterpret.Lexer
{
    public class Lexer
    {

        private string Source { get; set; }
        private int start = 0;
        private int current = 0;
        private int line = 1;
        private List<Token> tokens = new List<Token>();

        private static Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>
        {
            {"int", TokenType.Int},
            {"double", TokenType.Double},
            {"string", TokenType.String},
            {"if", TokenType.If},
            {"else", TokenType.Else},
            {"function", TokenType.Function},
            {"return", TokenType.Return},
            {"true", TokenType.True },
            {"false", TokenType.False},
            {"while", TokenType.While},
            {"void", TokenType.Void},
        };

        public List<Token> ScanTokens(string input)
        {
            if (input == null)
            {
                throw new Exception();
            }
                
            Source = input;

            while (!IsAtEnd())
            {
                start = current;
                ScanToken();
            }
            return tokens;
        }

        private void ScanToken()
        {
            char c = Advance();
            switch (c)
            {

                case ',':
                    tokens.Add(new Token(TokenType.Comma, line));
                    break;
                case ';':
                    tokens.Add(new Token(TokenType.Semi_Colon, line));
                    break;
                case '+':
                    tokens.Add(new Token(TokenType.Plus, line));
                    break;
                case '-':
                    tokens.Add(new Token(TokenType.Minus, line));
                    break;
                case '*':
                    tokens.Add(new Token(TokenType.Star, line));
                    break;
                case '/':
                    tokens.Add(new Token(TokenType.Slash, line));
                    break;
                case '(':
                    tokens.Add(new Token(TokenType.Left_Bracket, line));
                    break;
                case ')':
                    tokens.Add(new Token(TokenType.Right_Bracket, line));
                    break;
                case '[':
                    tokens.Add(new Token(TokenType.Left_Square_Bracket, line));
                    break;
                case ']':
                    tokens.Add(new Token(TokenType.Right_Square_Bracket, line));
                    break;
                case '{':
                    tokens.Add(new Token(TokenType.Left_Curly_Bracket, line));
                    break;
                case '}':
                    tokens.Add(new Token(TokenType.Right_Curly_Bracket, line));
                    break;
                case '.':
                    tokens.Add(new Token(TokenType.Dot, line));
                    break;
                case ':':
                    tokens.Add(new Token(TokenType.Colon, line));
                    break;
                case '=':
                    if (Match('='))
                        tokens.Add(new Token(TokenType.Equal_Equal, line));
                    else
                        tokens.Add(new Token(TokenType.Equal, line));
                    break;

                case '!':
                    if (Match('='))
                        tokens.Add(new Token(TokenType.Not_Equal, line));
                    else
                        Error("Unexpected character on line: " + line + " expected: '='");
                    break;

                case '>':
                    if (Match('='))
                        tokens.Add(new Token(TokenType.Greater_Equal, line));
                    else
                        tokens.Add(new Token(TokenType.Greater, line));
                    break;
                case '<':
                    if (Match('='))
                        tokens.Add(new Token(TokenType.Less_Equal, line));
                    else
                        tokens.Add(new Token(TokenType.Less, line));
                    break;

                case '"':
                    StringValue();
                    break;
                case ' ':
                case '\r':
                case '\t':
                    break;

                case '\n':
                    line++;
                    break;


                default:
                    if (IsDigit(c))
                    {
                        Number();
                    }
                    else if (IsAlpha(c))
                    {
                        Identifier();
                    }
                    else
                    {
                        Error("Unexpected character on line: " + line);
                    }
                    break;
            }
        }

        private void Identifier()
        {
            while (IsAlphaNumeric(Peek()))
                Advance();
            string text = Source.Substring(start, current - start);
            TokenType type;
            if (!keywords.TryGetValue(text, out type))
                tokens.Add(new Token(TokenType.Ident, text, line));
            else
                tokens.Add(new Token(type, line));
        }

        private bool IsAlpha(char c)
        {
            return (c >= 'a' && c <= 'z') ||
                   (c >= 'A' && c <= 'Z') ||
                    c == '_';
        }

        private bool IsAlphaNumeric(char c)
        {
            return IsAlpha(c) || IsDigit(c);
        }


        private void Number()
        {
            while (IsDigit(Peek()))
                Advance();

            if (Peek() == '.' && IsDigit(PeekNext()))
            {
                Advance();

                while (IsDigit(Peek()))
                    Advance();
                tokens.Add(new Token(TokenType.Real_Number, Source.Substring(start, current - start), line));
            }
            else
            {
                tokens.Add(new Token(TokenType.Number, Source.Substring(start, current - start), line));
            }

        }

        private void StringValue()
        {
            string value = "";
            while (Peek() != '"')
            {
                value += Advance();
            }
            tokens.Add(new Token(TokenType.String_Value, value, line));
            Advance();
        }

        private char Peek()
        {
            if (IsAtEnd()) return '\0';
            return Source[current];
        }

        private char PeekNext()
        {
            if (current + 1 >= Source.Length)
                return '\0';
            return Source[current + 1];
        }
        private bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private char Advance()
        {
            return Source[current++];
        }

        private bool IsAtEnd()
        {
            return current >= Source.Length;
        }

        private void Error(string input)
        {
            throw new Exception(input);
        }

        private bool Match(char expected)
        {
            if (IsAtEnd()) return false;
            if (Source[current] != expected) return false;

            current++;
            return true;
        }
    }
}
