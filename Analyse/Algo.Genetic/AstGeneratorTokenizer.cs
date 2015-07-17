using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.Parsing;

namespace Algo.Genetic
{
    public class AstGeneratorTokenizer
    {
        private TokenType _currentType;
        private uint _maxDepth, _currentDepth;
        private IList<TokenType> _tokens = new List<TokenType>();
        private int _pos;

        private bool IsEnd
        {
            get
            {
                return _currentDepth == _maxDepth;
            }
        }

        public AstGeneratorTokenizer(uint maxDepth)
        {
            _tokens = new List<TokenType>();
            _maxDepth = maxDepth;
            _currentType = TokenType.None;
        }

        public TokenType CurrentToken
        {
            get { return _currentType; }
        }

        public TokenType GetNextToken()
        {
            if (TokenType.EndOfInput != CurrentToken)
            {
                return _tokens.ElementAt(_pos++);
            }
            return TokenType.EndOfInput;
        }

        private TokenType GenerateNextToken()
        {
            if (IsEnd) { return _currentType = TokenType.EndOfInput; }

            return TokenType.None;
        }

    }
}
