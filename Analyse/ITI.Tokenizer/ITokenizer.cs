using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public interface ITokenizer
    {
        TokenType CurrentToken { get; }

        TokenType GetNextToken();

        bool MatchInteger(int expected);

        bool MatchInteger(out int value);

        bool MatchIdentifier(out string identifer);

        bool MatchIdentifier(string expected);
    }
}
