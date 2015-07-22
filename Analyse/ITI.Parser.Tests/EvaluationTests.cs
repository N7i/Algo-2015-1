using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ITI.Parsing.Tests
{
    [TestFixture]
    public class EvaluationTests
    {
        [TestCase( "6 - 4 + 5", 7.0 )]
        [TestCase( "6 - (4 + 5)", -3.0 )]
        [TestCase( "(6 - 4) + 5", 7.0 )]
        public void simple_expression( string expression, double expected )
        {
            Analyser a = new Analyser();
            Node e = a.Analyse( expression );
            double result = EvalVisitor.Evaluate( e );
            Assert.That( result, Is.EqualTo( expected ) );
        }

        [TestCase( "6 + -4", 2.0 )]
        [TestCase( "6 * -(4+6)", -60.0 )]
        [TestCase( "6 + - 4 - -(-5)", -3.0 )]
        public void expression_with_unary_minus( string expression, double expected )
        {
            Analyser a = new Analyser();
            Node e = a.Analyse( expression );
            double result = EvalVisitor.Evaluate( e );
            Assert.That( result, Is.EqualTo( expected ) );
        }

        [TestCase( 5.0, 8.0, "x+y", 13.0 )]
        [TestCase( 5.0, 8.0, "x*y", 40.0 )]
        [TestCase( 5.0, 8.0, "-(x*5+y/7)", -(5.0 * 5 + 8.0 / 7) )]
        public void expression_with_variables( double x, double y, string expression, double expected )
        {
            Analyser a = new Analyser();
            Node e = a.Analyse( expression );
            double result = EvalVisitor.Evaluate( e, name => name == "x" ? x : (name == "y" ? y : Double.NaN) );
            Assert.That( result, Is.EqualTo( expected ) );
        }

        [TestCase( "6 - 4 ? 5 : -5", 5.0 )]
        [TestCase( "6 - 6 ? 2 : -2", 2.0 )]
        [TestCase( "6 - 7 ? 2 : -2", -2.0 )]
        [TestCase( "6 - 7 ? 2 : (1 ? (-1*8 ? 2 : 54554) : 5)", 54554.0 )]
        public void ternary_operator( string expression, double expected )
        {
            Analyser a = new Analyser();
            Node e = a.Analyse( expression );
            double result = EvalVisitor.Evaluate( e );
            Assert.That( result, Is.EqualTo( expected ) );
        }


    }
}
