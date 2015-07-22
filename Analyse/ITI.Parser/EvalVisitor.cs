using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public class EvalVisitor : IAbstractVisitor<double>
    {
        readonly Func<string,double> _variableGetter;

        public EvalVisitor( Func<string,double> variableGetter )
        {
            _variableGetter = variableGetter;
        }

        public double Visit( ConstantNode n )
        {
            return n.Value;
        }

        public double Visit( BinaryOperatorNode n )
        {
            double left = this.VisitNode( n.Left );
            double right = this.VisitNode( n.Right );
            switch( n.Operator )
            {
                case TokenType.Plus: return left + right;
                case TokenType.Minus: return left - right;
                case TokenType.Mult: return left * right;
                default: return left / right;
            }
        }

        public double Visit( UnaryOperatorNode n )
        {
            Debug.Assert( n.Operator == TokenType.Minus, "Only Minus is currently supported as UnaryOperator." );
            return -this.VisitNode( n.Right );
        }

        public double Visit( ErrorNode n )
        {
            return Double.NaN;
        }

        public double Visit( VariableNode n )
        {
            return _variableGetter == null ? Double.NaN : _variableGetter( n.Name );
        }

        public double Visit( IfNode n )
        {
            return this.VisitNode( n.Condition ) >= 0 ? this.VisitNode( n.IfTrue ): this.VisitNode( n.IfFalse );
        }

        public static double Evaluate( Node n, Func<string, double> variableGetter = null )
        {
            return new EvalVisitor( variableGetter ).VisitNode( n );
        }

        public static double Evaluate( Node n, IDictionary<string, double> variables )
        {
            return new EvalVisitor( name => 
            {
                Double v;
                if( variables.TryGetValue( name, out v ) ) return v;
                return Double.NaN;
            } ).VisitNode( n );
        }

    }
}
