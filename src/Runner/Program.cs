using KerbalAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace ConsoleApp2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ; var source =
@"log(""first"")";
            try
            {
                ArgumentListSyntax
                var kGlobalStatement = KSyntaxFactory.GlobalStatement();
                var script = CSharpScript.Create(source);
                var compilation = script.GetCompilation().SyntaxTrees.FirstOrDefault().GetCompilationUnitRoot();
                var globalStatements = compilation.DescendantNodes().OfType<GlobalStatementSyntax>().ToList();
                var kTreeBuilder = new KTreeBuilder();
                var kCompilation = kTreeBuilder.CreateCompilation(globalStatements);
                var nodes = compilation.DescendantNodes().ToList();
                var kNodes = kCompilation.DescendantNodes().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); //temp
            }
        }
    }
}