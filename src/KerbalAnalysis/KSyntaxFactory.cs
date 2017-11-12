using KerbalAnalysis.Nodes;
using System.Collections.Generic;
using System;

namespace KerbalAnalysis
{
    public static class KSyntaxFactory
    {
        public static GlobalStatementNode GlobalStatement()
        {
            var globalStatement = new GlobalStatementNode();

            return globalStatement;
        }

        public static KSyntaxToken Token(KSyntaxKind openParenToken, int startIndex = 0, int endIndex = 0)
        {
            string text;
            switch (openParenToken)
            {
                case KSyntaxKind.OpenParenToken:
                    text = "(";
                    break;
                case KSyntaxKind.CloseParenToken:
                    text = ")";
                    break;
                case KSyntaxKind.Period:
                    text = ".";
                    break;
                case KSyntaxKind.SetKeyword:
                    text = "set";
                    break;
                case KSyntaxKind.ToKeyword:
                    text = "to";
                    break;
                default:
                    throw new KeyNotFoundException($"Unable to find token text value for '{openParenToken}'");
            }
            return new KSyntaxToken(openParenToken, text) { StartIndex = startIndex, EndIndex = endIndex };
        }

        public static ExpressionStatementNode ExpressionStatement()
        {
            return new ExpressionStatementNode();
        }

        public static InvocationExpressionNode InvocationExpression()
        {
            return new InvocationExpressionNode();
        }

        public static IdentifierNameExpressionNode IdentifierNameExpression(KSyntaxToken identifier)
        {
            var identifierNameExpression = new IdentifierNameExpressionNode(identifier);
            identifier.Parent = identifierNameExpression;
            return identifierNameExpression;
        }

        public static KSyntaxToken Identifier(string name)
        {
            return new KSyntaxToken(KSyntaxKind.IdentifierToken, name);
        }

        public static ArgumentListNode ArgumentList()
        {
            var openParenToken = Token(KSyntaxKind.OpenParenToken);
            var closeParenToken = Token(KSyntaxKind.OpenParenToken);
            return new ArgumentListNode().WithOpenParenToken(openParenToken).WithCloseParenToken(closeParenToken);
        }

        public static ArgumentNode Argument()
        {
            return new ArgumentNode();
        }

        public static LiteralExpressionNode LiteralExpression(KSyntaxKind stringLiteralExpression, KSyntaxToken token)
        {
            return new LiteralExpressionNode(stringLiteralExpression, token);
        }

        public static KSyntaxToken Literal(string stringLiteral)
        {
            return new KSyntaxToken(KSyntaxKind.StringLiteralToken, stringLiteral);
        }

        public static AssignmentExpressionNode AssignmentExpression(KSyntaxKind simpleAssignmentExpression, ExpressionNode left, ExpressionNode right)
        {
            throw new NotImplementedException();
        }

        public static AssignmentExpressionNode AssignmentExpression(KSyntaxKind simpleAssignmentExpression)
        {
            return new AssignmentExpressionNode();
        }
    }
}