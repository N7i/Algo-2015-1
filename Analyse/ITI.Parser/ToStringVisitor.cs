using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public class ToStringVisitor : AbstractVisitor
    {
        StringBuilder _buffer;

        public ToStringVisitor()
        {
            _buffer = new StringBuilder();
        }

        public override Node Visit( ConstantNode n )
        {
            _buffer.Append( n.Value );
            return n;
        }

        public override Node Visit( BinaryOperatorNode n )
        {
            _buffer.Append( '(' );
            this.VisitNode( n.Left );
            _buffer.Append( n.OperatorString );
            this.VisitNode( n.Right );
            _buffer.Append( ')' );
            return n;
        }

        public override Node Visit( UnaryOperatorNode n )
        {
            _buffer.Append( "-[" );
            this.VisitNode( n.Right );
            _buffer.Append( "]" );
            return n;
        }

        public override Node Visit( VariableNode n )
        {
            _buffer.Append( n.Name );
            return n;
        }

        public override Node Visit( ErrorNode n )
        {
            _buffer.Append( n.Message );
            return n;
        }

        public override string ToString()
        {
            return _buffer.ToString();
        }

        public static string Stringify( Node n )
        {
            var v = new ToStringVisitor();
            v.VisitNode( n );
            return v.ToString();
        }

    }
}
