using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ITI.Parsing.Tests
{
    [TestFixture]
    public class OptimizationTests
    {
        [TestCase( "6 + - - 4", "(6+4)" )]
        [TestCase( "x + -(-y)", "(x+y)" )]
        [TestCase( "x + -4", "(x+-4)" )]
        [TestCase( "x + - -- -- 4", "(x+-4)" )]
        public void useless_minus( string expression, string rewritten )
        {
            Analyser a = new Analyser();
            Node e = a.Analyse( expression );
            var optimizer = new RemoveUselessUnaryMinusVisitor();
            Node eOptimized = optimizer.VisitNode( e );
            Assert.That( ToStringVisitor.Stringify( eOptimized ), Is.EqualTo( rewritten ) );
        }

        [TestCase( "3 - 7 + x", "(-4+x)" )]
        [TestCase( "x + 3 - 7", "(-4+x)" )]
        [TestCase( "3 + x - 7", "(-4+x)" )]
        [TestCase( "3 + - x - 7", "(-4-x)" )]
        [TestCase( "30 * x / 6", "(5*x)" )]
        [TestCase( "3 *(x - 7)", "(3*(-7+x))" )]
        [TestCase( "3 *(x - 7 + 5 - (5*1))", "(3*(-7+x))" )]
        public void constant_resolution( string expression, string rewritten )
        {
            Analyser a = new Analyser();
            Node e = a.Analyse( expression );
            var o1 = new RemoveUselessUnaryMinusVisitor();
            var o2 = new ConstantResolverVisitor();
            Node e1 = o1.VisitNode( e );
            Node e2 = o2.VisitNode( e1 );
            Assert.That( ToStringVisitor.Stringify( e2 ), Is.EqualTo( rewritten ) );
        }
    }
}
