using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace KerbalAnalysis.Tests
{
    [TestClass]
    public class KSourceWriterTests
    {
        private KTreeBuilder _kTreeBuilder = new KTreeBuilder();
        private KSourceWriter _kSourceWriter = new KSourceWriter();

        [TestMethod]
        public void OneLineInvocationTest()
        {
            var source =
@"log(""first"")";
            var script = CSharpScript.Create(source);
            var compilation = script.GetCompilation().SyntaxTrees.FirstOrDefault().GetCompilationUnitRoot();
            var globalStatements = compilation.DescendantNodes().OfType<GlobalStatementSyntax>().ToList();
            var kCompilation = _kTreeBuilder.CreateCompilation(globalStatements);

            var kSource = _kSourceWriter.GetSourceCode(kCompilation);

            Assert.AreEqual("log(\"first\").", kSource);
        }

        [TestMethod]
        public void TwoLineInvocationTest()
        {
            var source =
@"clearscreen()
print("" == HELLO WORLD == "")";
            var expectedOutput =
@"clearscreen.
print("" == HELLO WORLD == "").";
            var script = CSharpScript.Create(source);
            var compilation = script.GetCompilation().SyntaxTrees.FirstOrDefault().GetCompilationUnitRoot();
            var globalStatements = compilation.DescendantNodes().OfType<GlobalStatementSyntax>().ToList();
            var kCompilation = _kTreeBuilder.CreateCompilation(globalStatements);

            var kSource = _kSourceWriter.GetSourceCode(kCompilation);

            Assert.AreEqual(expectedOutput, kSource);
        }

        [TestMethod]
        public void AssignmentExpressionTest()
        {
            var source =
@"throttle = 1.0";
            var expectedOutput =
@"set throttle to 1.0.";
            var script = CSharpScript.Create(source);
            var compilation = script.GetCompilation().SyntaxTrees.FirstOrDefault().GetCompilationUnitRoot();
            var globalStatements = compilation.DescendantNodes().OfType<GlobalStatementSyntax>().ToList();
            var kCompilation = _kTreeBuilder.CreateCompilation(globalStatements);

            var kSource = _kSourceWriter.GetSourceCode(kCompilation);

            Assert.AreEqual(expectedOutput, kSource);
        }
    }
}