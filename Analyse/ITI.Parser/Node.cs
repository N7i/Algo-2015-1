using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public abstract class Node
    {
        public abstract T Accept<T>( IAbstractVisitor<T> v );

    }
}
