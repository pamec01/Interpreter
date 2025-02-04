Interpret pro jednoduchý jazyk se syntaxí jazyka c.

_**CompilerInterpret/Grammar**_ 

obsahuje gramatiku jazyka a příklady syntaxe


_**CompilerInterpret/Lexer**_ 

lexer převede text na jednotlivé tokeny, např. "==" nebo "int"

výsledné objekty obsahují informace o tokenech např. číslo řádku pro výpis chyb nebo typ tokenu _TokenType_


_**CompilerInterpret/Parser**_ 

převezme zpracované tokeny a vrátí AST v podobě listu statementů

zároveň vytvoří tabulku proměnných _VariableTable_, která uchovává proměnné v rámci celého běhu programu


_**CompilerInterpret/Interpreter**_ 
interpretuje jednotlivé statementy
