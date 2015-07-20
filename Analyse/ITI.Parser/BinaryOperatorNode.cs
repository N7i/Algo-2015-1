﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public class BinaryOperatorNode : Node
    {
        public BinaryOperatorNode( TokenType oper, Node left, Node right )
        {
            if( (oper & TokenType.IsBinaryOperator) == 0 )  throw new ArgumentException( "Must be e binary operator", "oper" );
            if( left == null ) throw new ArgumentNullException( "left" );
            if( right == null ) throw new ArgumentNullException( "right" );
            Operator = oper;
            Left = left;
            Right = right;
        }

        public TokenType Operator { get; private set; }
        
        public Node Left { get; private set; }
        
        public Node Right { get; private set; }

        public override string ToString()
        {
            string oper;
            switch( Operator )
            {
                case TokenType.Minus: oper = "-"; break;
                case TokenType.Plus: oper = "+"; break;
                case TokenType.Mult: oper = "*"; break;
                case TokenType.Div: oper = "/"; break;
                default: throw new Exception( "Invalid Operator." );
            }
            return String.Format( "[{0},{1},{2}]", oper, Left, Right );
        }
    }
}
