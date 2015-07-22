using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public abstract class AbstractVisitor : IAbstractVisitor<Node>
    {
        public virtual Node Visit( ConstantNode n )
        {
            return n;
        }

        public virtual Node Visit( BinaryOperatorNode n )
        {
            var newLeft = this.VisitNode( n.Left );
            var newRight = this.VisitNode( n.Right );
            if( newLeft == n.Left && newRight == n.Right ) return n;
            return new BinaryOperatorNode( n.Operator, newLeft, newRight );
        }

        public virtual Node Visit( ErrorNode n )
        {
            return n;
        }

        public virtual Node Visit( UnaryOperatorNode n )
        {
            var newRight = this.VisitNode( n.Right );
            if( newRight == n.Right ) return n;
            return new UnaryOperatorNode( n.Operator, newRight );
        }

        public virtual Node Visit( IfNode n )
        {
            var newC = this.VisitNode( n.Condition );
            var newT = this.VisitNode( n.IfTrue );
            var newF = this.VisitNode( n.IfFalse );
            if( newC == n.Condition && newT == n.IfTrue && newF == n.IfFalse ) return n;
            return new IfNode( newC, newT, newF );
        }

       public virtual Node Visit( VariableNode n )
        {
            return n;
        }

    }
}
