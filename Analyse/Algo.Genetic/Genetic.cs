using ITI.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.Genetic
{
    public class Genetic
    {

        private Node GenerateRndAst(int depth)
        {
            Node root = null;
            for (var i = 0; i < depth; ++i)
            {
                Random r = new Random();
                int rInt = r.Next(0, 6); 

                switch(rInt)
                {
                    // Constant
                    case 0:
                        Random cr = new Random();
                        double constant = cr.NextDouble() * int.MaxValue;


                        break;
                    // Variable
                    case 1:

                        break;
                     // Binary
                    case 2:

                        break;
                    // Unary
                    case 3:

                        break;
                    // If
                    case 4:
                        break;
                   
                }
            }
            return root;
        }

    }
}
