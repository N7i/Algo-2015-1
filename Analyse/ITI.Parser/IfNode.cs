using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public class IfNode : Node
    {
        public IfNode( Node condition, Node ifTrue, Node ifFalse )
        {
            Condition = condition;
            IfTrue = ifTrue;
            IfFalse = ifFalse;
        }

        public Node Condition { get; private set; }
        public Node IfTrue { get; private set; }
        public Node IfFalse { get; private set; }

        public override T Accept<T>( IAbstractVisitor<T> v )
        {
            return v.Visit( this );
        }

        public override string ToString()
        {
            return String.Format( "({0}?{1}:{2})", Condition, IfTrue, IfFalse );
        }
    }
}
