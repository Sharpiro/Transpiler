using KerbalAnalysis;
using KerbalAnalysis.Nodes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    internal class Program
    {
        private static List<GlobalStatementNode> _statements = new List<GlobalStatementNode>();

        private static void Main(string[] args)
        {
            var source =
@"log(""first"")";
            try
            {
                var kGlobalStatement = KSyntaxFactory.GlobalStatement();
                var script = CSharpScript.Create(source);
                var root = script.GetCompilation().SyntaxTrees.FirstOrDefault().GetCompilationUnitRoot();
                var globalStatements = root.DescendantNodes().OfType<GlobalStatementSyntax>().ToList();
                ParseGlobalStatements(globalStatements);
                var nodes = globalStatements.First().DescendantNodes().ToList();
                var kNodes = _statements.FirstOrDefault().DescendantNodes().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); //temp
            }
        }

        private static void ParseGlobalStatements(List<GlobalStatementSyntax> globalStatements)
        {
            foreach (var globalStatement in globalStatements)
            {
                var kGlobalStatement = new GlobalStatementNode();
                switch (globalStatement.Statement.Kind())
                {
                    //case SyntaxKind.ForStatement:
                    //    ParseForStatement(globalStatement.Statement as ForStatementSyntax, kGlobalStatement);
                    //    break;
                    case SyntaxKind.ExpressionStatement:
                        ParseExpressionStatement(globalStatement.Statement as ExpressionStatementSyntax, kGlobalStatement);
                        break;
                    default:
                        throw new KeyNotFoundException($"couldn't find statement with name '{globalStatement.Kind()}'");
                }
                _statements.Add(kGlobalStatement);
            }
        }

        //private static void ParseForStatement(ForStatementSyntax forStatement, GlobalStatementNode newKGlobalStatement)
        //{
        //    var localVarName = "countdown";
        //    var initValue = 10;
        //    var conditionalExpression = "countdown = 0";
        //    var statement = $"FROM {{local {localVarName} is {initValue}.}} {conditionalExpression} STEP {{SET {localVarName} to {localVarName} - 1.}} DO";
        //}

        private static void ParseExpressionStatement(ExpressionStatementSyntax expressionStatement, GlobalStatementNode newKGlobalStatement)
        {
            var kExpressionStatement = KSyntaxFactory.ExpressionStatement();
            newKGlobalStatement.WithStatement(kExpressionStatement);
            ParseExpression(expressionStatement.Expression, kExpressionStatement);
        }

        private static void ParseExpression(ExpressionSyntax expression, KNode parent)
        {
            switch (expression.Kind())
            {
                case SyntaxKind.InvocationExpression:
                    ParseInvocationExpression(expression as InvocationExpressionSyntax, parent as ExpressionStatementNode);
                    break;
                case SyntaxKind.StringLiteralExpression:
                    ParseLiteralExpression(expression as LiteralExpressionSyntax, parent as ArgumentNode);
                    break;
                case SyntaxKind.IdentifierName:
                    ParseIdentifierNameExpression(expression as IdentifierNameSyntax, parent as InvocationExpressionNode);
                    break;
                default:
                    throw new KeyNotFoundException($"couldn't find expression with kind '{expression.Kind()}'");
            }
        }

        private static void ParseLiteralExpression(LiteralExpressionSyntax literalExpression, ArgumentNode kExpressionStatement)
        {
            var stringLiteral = literalExpression.Token.Text;
            var kLiteralExpression = KSyntaxFactory.LiteralExpression(KSyntaxKind.StringLiteralExpression, KSyntaxFactory.Literal(stringLiteral));
            kExpressionStatement.WithExpression(kLiteralExpression);
        }

        private static void ParseInvocationExpression(InvocationExpressionSyntax expression, ExpressionStatementNode kExpressionStatement)
        {
            var invocationExpression = KSyntaxFactory.InvocationExpression();
            kExpressionStatement.WithExpression(invocationExpression);
            ParseExpression(expression.Expression, invocationExpression);
            ParseArgumentList(expression.ArgumentList, invocationExpression);
        }

        private static void ParseIdentifierNameExpression(IdentifierNameSyntax identifierNameSyntax, InvocationExpressionNode invocationExpression)
        {
            var name = identifierNameSyntax.Identifier.Text;
            var kIdentifierExpression = KSyntaxFactory.IdentifierNameExpression(KSyntaxFactory.Identifier(name));
            invocationExpression.WithExpression(kIdentifierExpression);
        }

        private static void ParseArgumentList(ArgumentListSyntax argumentList, InvocationExpressionNode invocationExpression)
        {
            var kArgumentList = KSyntaxFactory.ArgumentList();
            invocationExpression.WithArgumentList(kArgumentList);

            foreach (var argument in argumentList.Arguments)
            {
                ParseArgument(argument, invocationExpression.ArgumentList);
            }
        }

        private static void ParseArgument(ArgumentSyntax argument, ArgumentListNode argumentList)
        {
            var kArgument = KSyntaxFactory.Argument();
            ParseExpression(argument.Expression, kArgument);
            argumentList.AddArgument(kArgument);
        }
    }
}