using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public class RemoveUselessUnaryMinusVisitor : AbstractVisitor
    {
        public override Node Visit( UnaryOperatorNode n )
        {
            Node visited = this.VisitNode( n.Right );
            if( n.Operator == TokenType.Minus )
            {
                if( visited is UnaryOperatorNode )
                {
                    UnaryOperatorNode rMinus = (UnaryOperatorNode)visited;
                    if( rMinus.Operator == TokenType.Minus )
                    {
                        return rMinus.Right;
                    }
                }
                else if( visited is ConstantNode )
                {
                    ConstantNode vC = (ConstantNode)visited;
                    return new ConstantNode( -vC.Value );
                }
            }
            if( visited == n.Right ) return n;
            return new UnaryOperatorNode( n.Operator, visited );
        }
    }
}
