using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public class ErrorNode : Node
    {
        public ErrorNode( string message )
        {
            Message = message;
        }

        public string Message { get; private set; }

        public override string ToString()
        {
            return "Error: " + Message;
        }

        public override T Accept<T>( IAbstractVisitor<T> v )
        {
            return v.Visit( this );
        }

    }
}
