using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace KerbalAnalysis.Tests
{
    [TestClass]
    public class KTreeBuilderTests
    {
        private KTreeBuilder _kTreeBuilder = new KTreeBuilder();

        [TestMethod]
        public void NodesTest()
        {
            ; var source =
@"log(""first"")";
            var script = CSharpScript.Create(source);
            var compilation = script.GetCompilation().SyntaxTrees.FirstOrDefault().GetCompilationUnitRoot();
            var globalStatements = compilation.DescendantNodes().OfType<GlobalStatementSyntax>().ToList();
            var kCompilation = _kTreeBuilder.CreateCompilation(globalStatements);
            var nodes = compilation.DescendantNodes().ToList();
            var nodesAndTokens = compilation.DescendantNodesAndTokens();
            var kNodes = kCompilation.DescendantNodes().ToList();
            Assert.AreEqual(nodes.Count, kNodes.Count);

            for (var i = 0; i < nodes.Count; i++)
            {
                Assert.AreEqual((int)nodes[i].Kind(), (int)kNodes[i].Kind);
            }
        }

        [TestMethod]
        public void TokensTest()
        {
            ; var source =
@"log(""first"")";
            var script = CSharpScript.Create(source);
            var compilation = script.GetCompilation().SyntaxTrees.FirstOrDefault().GetCompilationUnitRoot();
            var globalStatements = compilation.DescendantNodes().OfType<GlobalStatementSyntax>().ToList();
            var kCompilation = _kTreeBuilder.CreateCompilation(globalStatements);
            var tokens = compilation.DescendantTokens().ToList();
            var kTokens = kCompilation.DescendantTokens().ToList();
            var temp = SyntaxKind.DisabledTextTrivia;

            Assert.AreEqual(tokens.Count, kTokens.Count);

            for (var i = 0; i < tokens.Count; i++)
            {
                Assert.AreEqual((int)tokens[i].Kind(), (int)kTokens[i].Kind);
            }
        }
    }
}