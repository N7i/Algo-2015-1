using ITI.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.Genetic
{
    public class GeneticHelper
    {

        public static Node GenerateRndAst(int maxDepth)
        {
            return new BinaryOperatorNode(
                GenerateRndOperator(),
                GenerateRndNode(maxDepth, 0),
                GenerateRndNode(maxDepth, 0)
            );
        }

        private static Node GenerateRndNode(int maxDepth, int depth)
        {
            if (depth == maxDepth)
            {
                return new ConstantNode(GenerateRndConst());
            }

            Random rnd = new Random();
            int rValue = rnd.Next(1,3);


            switch(rValue)
            {
                case 1:
                    return new BinaryOperatorNode(
                        GenerateRndOperator(),
                        GenerateRndNode(maxDepth, depth + 1),
                        GenerateRndNode(maxDepth, depth + 1)
                    );
                case 2:
                    return new UnaryOperatorNode(
                            TokenType.Minus,
                            GenerateRndNode(maxDepth, depth + 1)
                    );
                default:
                    return new ConstantNode(GenerateRndConst());
                
            }
        }

        private static TokenType GenerateRndOperator()
        {
            Random rnd = new Random();
            int rValue = rnd.Next(1,3);

            switch (rValue)
            {
                case 1:
                    return TokenType.Plus;
                case 2:
                    return TokenType.Minus;
                default:
                   return TokenType.Mult;
            }
        }
        
        private static int GenerateRndConst()
        {
            Random rnd = new Random();
            return rnd.Next(0, int.MaxValue);
        }

    }
}
