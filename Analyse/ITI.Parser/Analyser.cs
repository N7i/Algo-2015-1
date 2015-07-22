using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public class Analyser
    {

        public Node Analyse( string s )
        {
            return Analyse( new Tokenizer( s ) );
        }

        public Node Analyse( Tokenizer tokenizer )
        {
            return SuperExpression( tokenizer );
        }

        Node SuperExpression( Tokenizer tokenizer )
        {
            Node e = Expression( tokenizer );
            if( !tokenizer.Match( TokenType.QuestionMark ) ) return e;
            Node ifTrue = Expression( tokenizer );
            if( !tokenizer.Match( TokenType.Colon ) ) return new ErrorNode( "Expected ':'." );
            Node ifFalse = Expression( tokenizer );
            return new IfNode( e, ifTrue, ifFalse );
        }

        // Before (from TD):      expression --> term +/- expression | term
        // After (some thoughts): expression --> term [+/- term]*
        Node Expression( Tokenizer tokenizer )
        {
            Node left = Term( tokenizer );
            while( !(left is ErrorNode) )
            {
                TokenType oper = tokenizer.CurrentToken;
                if( !tokenizer.Match( TokenType.Plus ) && !tokenizer.Match( TokenType.Minus ) ) break;
                left = new BinaryOperatorNode( oper, left, Term( tokenizer ) );
            }
            return left;
        }

        Node Term( Tokenizer tokenizer )
        {
            Node left = Factor( tokenizer );
            while( !(left is ErrorNode) )
            {
                TokenType oper = tokenizer.CurrentToken;
                if( !tokenizer.Match( TokenType.Mult ) && !tokenizer.Match( TokenType.Div ) ) break;
                left = new BinaryOperatorNode( oper, left, Factor( tokenizer ) );
            }
            return left;
        }

        Node Factor( Tokenizer tokenizer )
        {
            if( tokenizer.Match( TokenType.Minus ) ) return new UnaryOperatorNode( TokenType.Minus, Factor( tokenizer ) );
            return PositiveFactor( tokenizer );
        }
        Node PositiveFactor( Tokenizer tokenizer )
        {
            int value;
            if( tokenizer.MatchInteger( out value ) ) return new ConstantNode( value );
            
            string variableName;
            if( tokenizer.MatchIdentifier( out variableName ) ) return new VariableNode( variableName );

            if( !tokenizer.Match( TokenType.OpenPar ) ) return new ErrorNode( "Expected constant or identifier or opening parenthesis." );
            Node e = SuperExpression( tokenizer );
            if( e is ErrorNode ) return e;
            if( !tokenizer.Match( TokenType.ClosePar ) ) return new ErrorNode( "Expected closing parenthesis." );
            return e;
        }

    }
}
