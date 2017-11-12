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
                ExpressionStatementNode expressionStatement;
                switch (globalStatement.Statement.Kind())
                {
                    //case SyntaxKind.ForStatement:
                    //    ParseForStatement(globalStatement.Statement as ForStatementSyntax, kGlobalStatement);
                    //    break;
                    case SyntaxKind.ExpressionStatement:
                        expressionStatement = ParseExpressionStatement(globalStatement.Statement as ExpressionStatementSyntax);
                        break;
                    default:
                        throw new KeyNotFoundException($"couldn't find statement with name '{globalStatement.Kind()}'");
                }
                kGlobalStatement.WithStatement(expressionStatement);
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

        private ExpressionStatementNode ParseExpressionStatement(ExpressionStatementSyntax expressionStatement)
        {
            var expression = ParseExpression(expressionStatement.Expression);
            var kExpressionStatement = KSyntaxFactory.ExpressionStatement().WithExpression(expression);
            return kExpressionStatement;
        }

        private ExpressionNode ParseExpression(ExpressionSyntax expression)
        {
            ExpressionNode kExpression;
            switch (expression.Kind())
            {
                case SyntaxKind.InvocationExpression:
                    kExpression = ParseInvocationExpression(expression as InvocationExpressionSyntax);
                    break;
                case SyntaxKind.NumericLiteralExpression:
                case SyntaxKind.StringLiteralExpression:
                    kExpression = ParseLiteralExpression(expression as LiteralExpressionSyntax);
                    break;
                case SyntaxKind.IdentifierName:
                    kExpression = ParseIdentifierNameExpression(expression as IdentifierNameSyntax);
                    break;
                case SyntaxKind.SimpleAssignmentExpression:
                    kExpression = ParseSimpleAssignmentExpression(expression as AssignmentExpressionSyntax);
                    break;
                default:
                    throw new KeyNotFoundException($"couldn't find expression with kind '{expression.Kind()}'");
            }
            return kExpression;
        }

        private AssignmentExpressionNode ParseSimpleAssignmentExpression(AssignmentExpressionSyntax assignmentExpression)
        {
            var set = KSyntaxFactory.Token(KSyntaxKind.SetKeyword);
            var leftExpression = ParseExpression(assignmentExpression.Left);
            var to = KSyntaxFactory.Token(KSyntaxKind.ToKeyword);
            var rightExpression = ParseExpression(assignmentExpression.Right);
            var kAssignmentExpression = KSyntaxFactory.AssignmentExpression(KSyntaxKind.SimpleAssignmentExpression)
                .WithSet(set).WithLeft(leftExpression).WithTo(to).WithRight(rightExpression);
            return kAssignmentExpression;
        }

        private LiteralExpressionNode ParseLiteralExpression(LiteralExpressionSyntax literalExpression)
        {
            var stringLiteral = literalExpression.Token.Text;
            var kLiteralExpression = KSyntaxFactory.LiteralExpression((KSyntaxKind)literalExpression.Kind(), KSyntaxFactory.Literal(stringLiteral));
            return kLiteralExpression;
        }

        private InvocationExpressionNode ParseInvocationExpression(InvocationExpressionSyntax invocationExpressionSyntax)
        {
            var expression = ParseExpression(invocationExpressionSyntax.Expression);
            var argumentList = ParseArgumentList(invocationExpressionSyntax.ArgumentList);
            var invocationExpression = KSyntaxFactory.InvocationExpression().WithExpression(expression).WithArgumentList(argumentList);
            return invocationExpression;
        }

        private IdentifierNameExpressionNode ParseIdentifierNameExpression(IdentifierNameSyntax identifierNameSyntax)
        {
            var name = identifierNameSyntax.Identifier.Text;
            var kIdentifierExpression = KSyntaxFactory.IdentifierNameExpression(KSyntaxFactory.Identifier(name));
            return kIdentifierExpression;
        }

        private ArgumentListNode ParseArgumentList(ArgumentListSyntax argumentList)
        {
            var kArgumentList = KSyntaxFactory.ArgumentList();
            var sourceOpenParenSpan = argumentList.OpenParenToken.Span;
            var sourceCloseParenSpan = argumentList.CloseParenToken.Span;
            kArgumentList.WithOpenParenToken(KSyntaxFactory.Token(KSyntaxKind.OpenParenToken, sourceOpenParenSpan.Start, sourceOpenParenSpan.End));

            foreach (var argument in argumentList.Arguments)
            {
                var kArgument = ParseArgument(argument);
                kArgumentList.AddArguments(kArgument);
            }

            kArgumentList.WithCloseParenToken(KSyntaxFactory.Token(KSyntaxKind.CloseParenToken, sourceCloseParenSpan.Start, sourceCloseParenSpan.End));
            return kArgumentList;
        }

        private ArgumentNode ParseArgument(ArgumentSyntax argument)
        {
            var expression = ParseExpression(argument.Expression);
            var kArgument = KSyntaxFactory.Argument().WithExpression(expression);
            return kArgument;
        }
    }
}