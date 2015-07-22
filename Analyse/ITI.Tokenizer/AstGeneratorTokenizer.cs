using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.Parsing;

namespace Algo.Genetic
{
    public class AstGeneratorTokenizer : AbstractTokenizer
    {
        private TokenType _currentType;
        private uint _maxDepth, _currentDepth;
        private IList<TokenType> _tokens = new List<TokenType>();
        private int _pos;

        #region Buffer
        int _currentIntValue;
        string _currentIdentifierValue;
        #endregion Buffer

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

        private TokenType GenerateNextToken()
        {
            if (IsEnd) { return _currentType = TokenType.EndOfInput; }
           

            return TokenType.None;
        }

        public override TokenType CurrentToken
        {
            get { return _currentType; }
        }

        public override TokenType GetNextToken()
        {
            if (TokenType.EndOfInput != CurrentToken)
            {
                return _tokens.ElementAt(_pos++);
            }
            return TokenType.EndOfInput;
        }

        public override bool MatchInteger(int expected)
        {
            if (CurrentToken == TokenType.Number && _currentIntValue == expected)
            {
                GetNextToken();
                return true;
            }
            return false;
        }

        public override bool MatchInteger(out int value)
        {
            value = default(int);
            if (CurrentToken == TokenType.Number)
            {
                value = _currentIntValue;
                GetNextToken();
                return true;
            }
            return false;
        }

        public override bool MatchIdentifier(string expected)
        {
            if (CurrentToken == TokenType.Identifier && 
                    _currentIdentifierValue == expected)
            {
                GetNextToken();
                return true;
            }
            return false;
        }

        public override bool MatchIdentifier(out string identifer)
        {
            identifer = default(string);
            if (CurrentToken == TokenType.Identifier)
            {
                identifer = _currentIdentifierValue;
                GetNextToken();
                return true;
            }
            return false;
        }
    }
}
