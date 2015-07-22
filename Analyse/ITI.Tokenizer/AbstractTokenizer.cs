using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public abstract class AbstractTokenizer : ITokenizer
    {
        public abstract TokenType CurrentToken { get; }

        public abstract TokenType GetNextToken();

        public virtual bool Match(TokenType t)
        {
            if (CurrentToken == t)
            {
                GetNextToken();
                return true;
            }
            return false;
        }

        public abstract bool MatchInteger(int expected);

        public abstract bool MatchInteger(out int value);

        public abstract bool MatchIdentifier(out string identifer);
        public abstract bool MatchIdentifier(string expected);
    }
}
