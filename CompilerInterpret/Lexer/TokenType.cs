using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerInterpret.Lexer
{
    public enum TokenType
    {

        Int, Double, Float, String, Void,
        Function, If, Else, Return, While,
        Plus, Minus, Star, Slash, Division_Sign,
        Dot, Colon, Semi_Colon, Comma, Equal,
        Equal_Equal, Not_Equal, Greater, Less, Greater_Equal, Less_Equal,
        True, False,
        Left_Bracket, Right_Bracket, Left_Curly_Bracket, Right_Curly_Bracket, Right_Square_Bracket, Left_Square_Bracket,
        Ident, Real_Number, Number, String_Value

    }
}
