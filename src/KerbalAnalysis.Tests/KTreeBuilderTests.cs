﻿using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace KerbalAnalysis.Tests
{
    [TestClass]
    public class KTreeBuilderTests
    {
        private KTreeBuilder _kTreeBuilder = new KTreeBuilder();

        [TestMethod]
        public void InvocationTest()
        {
            var source =
@"log(""first"")";
            TestNodes(source);
        }

        [TestMethod]
        public void AssignmentExpressionTest()
        {
            var source =
@"throttle = 1";
            TestNodes(source);
        }

        [TestMethod]
        public void VariableDeclarationTest()
        {
            var source =
@"int i = 0";
            TestNodes(source);
        }

        [TestMethod]
        public void AdditionTest()
        {
            var source =
@"countdown = countdown + 1";
            TestNodes(source);
        }

        private void TestNodes(string source)
        {
            var x = SyntaxKind.AbstractKeyword;
            var script = CSharpScript.Create(source);
            var compilation = script.GetCompilation().SyntaxTrees.FirstOrDefault().GetCompilationUnitRoot();
            var nodes = compilation.DescendantNodes().ToList();
            var kCompilation = _kTreeBuilder.CreateCompilation(compilation);
            var kNodes = kCompilation.DescendantNodes().ToList();

            for (var i = 0; i < nodes.Count; i++)
            {
                Assert.AreEqual((int)nodes[i].Kind(), (int)kNodes[i].Kind);
            }

            Assert.AreEqual(nodes.Count, kNodes.Count);
        }
    }
}