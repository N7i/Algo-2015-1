using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ITI.Parsing.Tests
{
    [TestFixture]
    public class TokenizerTests
    {

        [Test]
        public void basic_tokens_operators_parenthesis_and_numbers()
        {
            string s = " 4 * 8 + 7 (5)/8*47877 ";
            Tokenizer t = new Tokenizer( s );
            Assert.That( t.CurrentToken, Is.EqualTo( TokenType.Number ) );
            Assert.That( t.GetNextToken(), Is.EqualTo( TokenType.Mult ) );

            Assert.That( t.Match( TokenType.Mult ) );
            int v;
            Assert.That( t.MatchInteger( out v ) && v == 8 );
            Assert.That( t.Match( TokenType.Plus ) );
            Assert.That( t.MatchInteger( out v ) && v == 7 );
            Assert.That( t.Match( TokenType.OpenPar ) );
            Assert.That( t.MatchInteger( out v ) && v == 5 );
            Assert.That( t.Match( TokenType.ClosePar ) );
            Assert.That( t.Match( TokenType.Div ) );
            Assert.That( t.MatchInteger( out v ) && v == 8 );
            Assert.That( t.Match( TokenType.Mult ) );
            Assert.That( t.MatchInteger( out v ) && v == 47877 );
            Assert.That( t.CurrentToken, Is.EqualTo( TokenType.EndOfInput ) );
        }

        [Test]
        public void identifier_support()
        {
            string s = "4*a+6/delta";
            Tokenizer t = new Tokenizer( s );
            int four, six;
            string a, delta;
            Assert.That( t.MatchInteger( out four ) && four == 4 );
            Assert.That( t.Match( TokenType.Mult ) );
            Assert.That( t.MatchIdentifier( out a ) && a == "a" );
            Assert.That( t.Match( TokenType.Plus ) );
            Assert.That( t.MatchInteger( out six ) && six == 6 );
            Assert.That( t.Match( TokenType.Div ) );
            Assert.That( t.MatchIdentifier( out delta ) && delta == "delta" );
        }

        [Test]
        public void identifier_support_match_known()
        {
            string s = "when identifiers are known";
            Tokenizer t = new Tokenizer( s );
            Assert.That( t.MatchIdentifier( "when" ) );
            Assert.That( t.MatchIdentifier( "identifiers" ) );
            Assert.That( t.MatchIdentifier( "are" ) );
            Assert.That( t.MatchIdentifier( "known" ) );
            Assert.That( !t.MatchIdentifier( "whatever since we are at the end" ) );
        }
    }
}
