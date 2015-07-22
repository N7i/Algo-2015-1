using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ITI.Parsing.Tests
{
    [TestFixture]
    public class SimpleParserTests
    {
        [TestCase( "6 - (4 + 5)" )]
        public void simple_expression( string expression )
        {
            Analyser a = new Analyser();
            Node e = a.Analyse( expression );
            Assert.That( e, Is.InstanceOf<BinaryOperatorNode>() );
            var root = (BinaryOperatorNode)e;
            Assert.That( root.Operator, Is.EqualTo( TokenType.Minus ) );
            Assert.That( root.Left, Is.InstanceOf<ConstantNode>() );
            Assert.That( ((ConstantNode)root.Left).Value, Is.EqualTo( 6 ) );
            Assert.That( root.Right, Is.InstanceOf<BinaryOperatorNode>() );
            var right = (BinaryOperatorNode)root.Right;
            Assert.That( right.Operator, Is.EqualTo( TokenType.Plus ) );
        }

        [TestCase( "6 - 4 + 5", "[+,[-,6,4],5]" )]
        [TestCase( "6 - (4 + 5)", "[-,6,[+,4,5]]" )]
        [TestCase( "68 - 4 * 8)", "[-,68,[*,4,8]]" )]
        [TestCase( "68 * 4 + 8)", "[+,[*,68,4],8]" )]
        public void simple_expression( string expression, string treeRepresentation )
        {
            Analyser a = new Analyser();
            Node e = a.Analyse( expression );
            Assert.That( e.ToString(), Is.EqualTo( treeRepresentation ) );
        }

        [TestCase( "6 - 4 + 5", "((6-4)+5)" )]
        [TestCase( "68 * 4 + 8)", "((68*4)+8)" )]
        public void simple_expression_via_ToStringVisitor( string expression, string representation )
        {
            Analyser a = new Analyser();
            Node e = a.Analyse( expression );
            Assert.That( ToStringVisitor.Stringify( e ), Is.EqualTo( representation ) );
        }
    }
}
