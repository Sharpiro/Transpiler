using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
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
            var expectedOutput = "log(\"first\").";
            TestOutput(source, expectedOutput);
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
            TestOutput(source, expectedOutput);
        }

        [TestMethod]
        public void AssignmentExpressionTest()
        {
            var source =
@"throttle = 1.0";
            var expectedOutput =
@"set throttle to 1.0.";
            TestOutput(source, expectedOutput);
        }

        [TestMethod]
        public void VariableDeclarationTest()
        {
            var source =
@"int countdown = 10";
            var expectedOutput =
@"local countdown is 10.";
            TestOutput(source, expectedOutput);
        }

        [TestMethod]
        public void AdditionTest()
        {
            var source =
@"countdown = countdown + 1";
            var expectedOutput =
@"set countdown to countdown + 1.";
            TestOutput(source, expectedOutput);
        }

        [TestMethod]
        public void ForStatementTest()
        {
            var source =
@"for (int countdown = 0; countdown < 10; countdown++) { }";
            var expectedOutput =
@"from {local countdown is 0.} until countdown = 10 step {set countdown to countdown + 1.} do { }";
            TestOutput(source, expectedOutput);
        }

        private void TestOutput(string input, string expectedOutput)
        {
            var script = CSharpScript.Create(input);
            var compilation = script.GetCompilation().SyntaxTrees.FirstOrDefault().GetCompilationUnitRoot();
            var kCompilation = _kTreeBuilder.CreateCompilation(compilation);

            var kSource = _kSourceWriter.GetSourceCode(kCompilation);

            Assert.AreEqual(expectedOutput, kSource);
        }
    }
}