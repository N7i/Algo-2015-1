﻿using System;
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
    }
}