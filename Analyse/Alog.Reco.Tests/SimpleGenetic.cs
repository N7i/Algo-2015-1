using Algo.Genetic;
using ITI.Parsing;
using NUnit.Framework;
using System;

namespace Alog.Reco.Tests
{
    [TestFixture]
    public class SimpleGenetic
    {
        [Test]
        public void SimpleTest()
        {
            Node ast = GeneticHelper.GenerateRndAst(15);
            Console.WriteLine(
                ToStringVisitor.Stringify(ast));
        }
    }
}
