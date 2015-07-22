using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public class VariableNode : Node
    {
        public VariableNode( string name )
        {
            Name = name;
        }

        public string Name { get; private set; }

        public override string ToString()
        {
            return Name;
        }

        public override T Accept<T>( IAbstractVisitor<T> v )
        {
            return v.Visit( this );
        }

    }
}
