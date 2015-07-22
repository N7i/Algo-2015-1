using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    [Flags]
    public enum TokenType
    {
        None = 0,
        Identifier = 1 << 10,
        IsBracket = 1 << 11,
        IsBinaryOperator = 1 << 12,
        Number = 1 << 13,
        
        Plus = IsBinaryOperator + 0,
        Minus = IsBinaryOperator + 1,
        Mult = IsBinaryOperator + 2,
        Div = IsBinaryOperator + 3,
        
        OpenPar = IsBracket + 0,
        ClosePar = IsBracket + 1,

        QuestionMark = 1,
        Colon = 2,

        EndOfInput = 1 << 30,
        Error = 1 << 31
    }
}
