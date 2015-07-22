using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public class UnaryOperatorNode : Node
    {
        public UnaryOperatorNode( TokenType oper, Node right )
        {
            if( oper != TokenType.Minus ) throw new ArgumentException( "Must be Minus operator", "oper" );
            if( right == null ) throw new ArgumentNullException( "right" );
            Operator = oper;
            Right = right;
        }

        public TokenType Operator { get; private set; }
        
        public Node Right { get; private set; }
        
        public override string ToString()
        {
            return String.Format( "[-{0}]", Right );
        }

        public override T Accept<T>( IAbstractVisitor<T> v )
        {
            return v.Visit( this );
        }

    }
}
