using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public class ConstantResolverVisitor : AbstractVisitor
    {
        public override Node Visit( BinaryOperatorNode n )
        {
            var left = this.VisitNode( n.Left );
            var right = this.VisitNode( n.Right );
            // 1 - Trivial case: left & right are both constants, we can reduce them to a ConstantNode.
            var cLeft = left as ConstantNode;
            var cRight = right as ConstantNode;
            if( cLeft != null && cRight != null )
            {
                switch( n.Operator )
                {
                    case TokenType.Minus: return new ConstantNode( cLeft.Value - cRight.Value );
                    case TokenType.Plus: return new ConstantNode( cLeft.Value + cRight.Value );
                    case TokenType.Mult: return new ConstantNode( cLeft.Value * cRight.Value );
                    case TokenType.Div: return new ConstantNode( cLeft.Value / cRight.Value );
                }
                Debug.Fail( "never here." );
            }
            // 2 - Normalization: if cRight is a constant, transfer it to the left.
            TokenType oper = n.Operator;
            if( cRight != null )
            {
                Debug.Assert( cLeft == null );
                // It is easy for + or * since they are commutative.
                if( n.Operator == TokenType.Mult || n.Operator == TokenType.Plus )
                {
                    var temp = left;
                    left = right;
                    right = temp;
                    cLeft = cRight;
                    cRight = null;
                }
                // If it is a -, we must negate cRight and transform this '-' into '+'.
                else if( n.Operator == TokenType.Minus )
                {
                    oper = TokenType.Plus;
                    right = left;
                    left = cLeft = new ConstantNode( -cRight.Value );
                    cRight = null;
                }
                // If it is a /, we must invert cRight and transform this '/' into '*'.
                else 
                {
                    Debug.Assert( n.Operator == TokenType.Div );
                    oper = TokenType.Mult;
                    right = left;
                    left = cLeft = new ConstantNode( 1 / cRight.Value );
                    cRight = null;
                }
            }
            // Now that this node is normalized, if we have a constant, it is necessary on the left.
            if( cLeft != null )
            {
                if( oper == TokenType.Mult || oper == TokenType.Plus )
                {
                    var bRight = right as BinaryOperatorNode;
                    if( bRight != null && bRight.Operator == oper && bRight.Left is ConstantNode )
                    {
                        double otherValue = ((ConstantNode)bRight.Left).Value;
                        double composedValue;
                        if( oper == TokenType.Mult ) composedValue = cLeft.Value * otherValue;
                        else composedValue = cLeft.Value + otherValue;
                        return new BinaryOperatorNode( oper, new ConstantNode( composedValue ), bRight.Right );
                    }
                }
            }
            // 3 - Normal processing.
            if( oper == n.Operator && left == n.Left && right == n.Right ) return n;
            return new BinaryOperatorNode( oper, left, right );
        }
    }
}
