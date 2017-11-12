using KerbalAnalysis.Nodes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System;

namespace KerbalAnalysis
{
    public class KTreeBuilder
    {
        public CompilationUnitNode CreateCompilation(List<GlobalStatementSyntax> globalStatements)
        {
            var kCompilation = new CompilationUnitNode();
            var kGlobalStatements = ParseGlobalStatements(globalStatements);
            kCompilation.WithMembers(kGlobalStatements);
            return kCompilation;
        }

        private IEnumerable<GlobalStatementNode> ParseGlobalStatements(List<GlobalStatementSyntax> globalStatements)
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
                yield return kGlobalStatement;
            }
        }

        //private  void ParseForStatement(ForStatementSyntax forStatement, GlobalStatementNode newKGlobalStatement)
        //{
        //    var localVarName = "countdown";
        //    var initValue = 10;
        //    var conditionalExpression = "countdown = 0";
        //    var statement = $"FROM {{local {localVarName} is {initValue}.}} {conditionalExpression} STEP {{SET {localVarName} to {localVarName} - 1.}} DO";
        //}

        private void ParseExpressionStatement(ExpressionStatementSyntax expressionStatement, GlobalStatementNode newKGlobalStatement)
        {
            var kExpressionStatement = KSyntaxFactory.ExpressionStatement();
            newKGlobalStatement.WithStatement(kExpressionStatement);
            ParseExpression(expressionStatement.Expression, kExpressionStatement);
        }

        private void ParseExpression(ExpressionSyntax expression, KNode parent)
        {
            switch (expression.Kind())
            {
                case SyntaxKind.InvocationExpression:
                    ParseInvocationExpression(expression as InvocationExpressionSyntax, parent as ExpressionStatementNode);
                    break;
                case SyntaxKind.NumericLiteralExpression:
                case SyntaxKind.StringLiteralExpression:
                    ParseLiteralExpression(expression as LiteralExpressionSyntax, parent as ArgumentNode);
                    break;
                case SyntaxKind.IdentifierName:
                    ParseIdentifierNameExpression(expression as IdentifierNameSyntax, parent as InvocationExpressionNode);
                    break;
                case SyntaxKind.SimpleAssignmentExpression:
                    ParseSimpleAssignmentExpression(expression as AssignmentExpressionSyntax, parent as ExpressionStatementNode);
                    break;
                default:
                    throw new KeyNotFoundException($"couldn't find expression with kind '{expression.Kind()}'");
            }
        }

        private void ParseSimpleAssignmentExpression(AssignmentExpressionSyntax assignmentExpression, ExpressionStatementNode kExpressionStatement)
        {
            ExpressionNode left = null;
            ExpressionNode right = null;
            var kAssignmentExpression = KSyntaxFactory.AssignmentExpression(KSyntaxKind.SimpleAssignmentExpression);
            kExpressionStatement.WithExpression(kAssignmentExpression);
            ParseExpression(assignmentExpression.Left, kAssignmentExpression);
            ParseExpression(assignmentExpression.Right, kAssignmentExpression);

        }

        private void ParseLiteralExpression(LiteralExpressionSyntax literalExpression, ArgumentNode kExpressionStatement)
        {
            var stringLiteral = literalExpression.Token.Text;
            var kLiteralExpression = KSyntaxFactory.LiteralExpression((KSyntaxKind)literalExpression.Kind(), KSyntaxFactory.Literal(stringLiteral));
            kExpressionStatement.WithExpression(kLiteralExpression);
        }

        private void ParseInvocationExpression(InvocationExpressionSyntax expression, ExpressionStatementNode kExpressionStatement)
        {
            var invocationExpression = KSyntaxFactory.InvocationExpression();
            kExpressionStatement.WithExpression(invocationExpression);
            ParseExpression(expression.Expression, invocationExpression);
            ParseArgumentList(expression.ArgumentList, invocationExpression);
        }

        private void ParseIdentifierNameExpression(IdentifierNameSyntax identifierNameSyntax, InvocationExpressionNode invocationExpression)
        {
            var name = identifierNameSyntax.Identifier.Text;
            var kIdentifierExpression = KSyntaxFactory.IdentifierNameExpression(KSyntaxFactory.Identifier(name));
            invocationExpression.WithExpression(kIdentifierExpression);
        }

        private void ParseArgumentList(ArgumentListSyntax argumentList, InvocationExpressionNode invocationExpression)
        {
            var kArgumentList = KSyntaxFactory.ArgumentList();
            invocationExpression.WithArgumentList(kArgumentList);
            var sourceOpenParenSpan = argumentList.OpenParenToken.Span;
            var sourceCloseParenSpan = argumentList.CloseParenToken.Span;
            kArgumentList.WithOpenParenToken(KSyntaxFactory.Token(KSyntaxKind.OpenParenToken, sourceOpenParenSpan.Start, sourceOpenParenSpan.End));

            foreach (var argument in argumentList.Arguments)
            {
                ParseArgument(argument, invocationExpression.ArgumentList);
            }

            kArgumentList.WithCloseParenToken(KSyntaxFactory.Token(KSyntaxKind.CloseParenToken, sourceCloseParenSpan.Start, sourceCloseParenSpan.End));
        }

        private void ParseArgument(ArgumentSyntax argument, ArgumentListNode argumentList)
        {
            var kArgument = KSyntaxFactory.Argument();
            ParseExpression(argument.Expression, kArgument);
            argumentList.AddArgument(kArgument);
        }
    }
}