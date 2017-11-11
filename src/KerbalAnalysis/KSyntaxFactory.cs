using System;
using KerbalAnalysis.Nodes;
using System.Linq;

namespace KerbalAnalysis
{
    public static class KSyntaxFactory
    {
        public static GlobalStatementNode GlobalStatement()
        {
            var globalStatement = new GlobalStatementNode();
            //var expressionStatementNode = new ExpressionStatementNode();
            //globalStatement.Statement = expressionStatementNode;
            //var invocationExpression = new InvocationExpressionNode();
            //expressionStatementNode.Expression = invocationExpression;
            //var identifierToken = new SyntaxToken
            //{
            //    Kind = KSyntaxKind.IdentifierToken,
            //    Text = "log",
            //};
            //var identifierNameExpression = new IdentifierNameExpressionNode(identifierToken);
            //invocationExpression.Expression = identifierNameExpression;
            //invocationExpression.ArgumentList = new ArgumentListNode();
            //var argument = new ArgumentNode();
            //var stringToken = new SyntaxToken
            //{
            //    Kind = KSyntaxKind.StringLiteralToken,
            //    Text = "first"
            //};
            //argument.Expression = new LiteralExpressionNode(KSyntaxKind.StringLiteralExpression, stringToken);

            //invocationExpression.ArgumentList.Arguments.Add(
            //    argument
            //);


            return globalStatement;
        }

        public static ExpressionStatementNode ExpressionStatement()
        {
            return new ExpressionStatementNode();
        }

        public static InvocationExpressionNode InvocationExpression()
        {
            return new InvocationExpressionNode();
        }

        public static IdentifierNameExpressionNode IdentifierNameExpression(SyntaxToken identifier)
        {
            var identifierNameExpression = new IdentifierNameExpressionNode(identifier);
            identifier.Parent = identifierNameExpression;
            return identifierNameExpression;
        }

        public static SyntaxToken Identifier(string name)
        {
            return new SyntaxToken
            {
                Text = name
            };
        }

        public static ArgumentListNode ArgumentList()
        {
            return new ArgumentListNode();
        }

        public static ArgumentNode Argument()
        {
            return new ArgumentNode();
        }

        public static LiteralExpressionNode LiteralExpression(KSyntaxKind stringLiteralExpression, SyntaxToken token)
        {
            return new LiteralExpressionNode(KSyntaxKind.StringLiteralExpression, token);

        }

        public static SyntaxToken Literal(string stringLiteral)
        {
            return new SyntaxToken
            {
                Text = stringLiteral
            };
        }
    }
}