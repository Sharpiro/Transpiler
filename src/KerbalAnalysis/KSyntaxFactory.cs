using KerbalAnalysis.Nodes;
using System.Collections.Generic;
using System;
using System.Linq;

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
                case KSyntaxKind.OpenBraceToken:
                    text = "{";
                    break;
                case KSyntaxKind.CloseBraceToken:
                    text = "}";
                    break;
                case KSyntaxKind.Period:
                    text = ".";
                    break;
                case KSyntaxKind.LessThanToken:
                    text = "<";
                    break;
                case KSyntaxKind.GreaterThanToken:
                    text = ">";
                    break;
                case KSyntaxKind.LessThanEqualsToken:
                    text = "<=";
                    break;
                case KSyntaxKind.GreaterThanEqualsToken:
                    text = ">=";
                    break;
                case KSyntaxKind.PlusToken:
                    text = "+";
                    break;
                case KSyntaxKind.MinusToken:
                    text = "-";
                    break;
                case KSyntaxKind.SetKeyword:
                    text = "set";
                    break;
                case KSyntaxKind.ToKeyword:
                    text = "to";
                    break;
                case KSyntaxKind.LocalKeyword:
                    text = "local";
                    break;
                case KSyntaxKind.IsKeyword:
                    text = "is";
                    break;
                case KSyntaxKind.ForKeyword:
                    text = "for";
                    break;
                case KSyntaxKind.FromKeyword:
                    text = "from";
                    break;
                case KSyntaxKind.UntilKeyword:
                    text = "until";
                    break;
                case KSyntaxKind.StepKeyword:
                    text = "step";
                    break;
                case KSyntaxKind.DoKeyword:
                    text = "do";
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

        public static FieldDeclarationNode FieldDeclaration()
        {
            return new FieldDeclarationNode();
        }

        public static KSyntaxToken Identifier(string name)
        {
            return new KSyntaxToken(KSyntaxKind.IdentifierToken, name);
        }

        public static VariableDeclarationNode VariableDeclaration()
        {
            return new VariableDeclarationNode();
        }

        public static ArgumentListNode ArgumentList()
        {
            var openParenToken = Token(KSyntaxKind.OpenParenToken);
            var closeParenToken = Token(KSyntaxKind.OpenParenToken);
            return new ArgumentListNode().WithOpenParenToken(openParenToken).WithCloseParenToken(closeParenToken);
        }

        public static VariableDeclaratorNode VariableDeclarator()
        {
            return new VariableDeclaratorNode();
        }

        public static ArgumentNode Argument()
        {
            return new ArgumentNode();
        }

        public static PredefinedTypeNode PredefinedType(KSyntaxToken keyword)
        {
            return new PredefinedTypeNode().WithKeyword(keyword);
        }

        public static LiteralExpressionNode LiteralExpression(KSyntaxKind literalExpressionKind, KSyntaxToken token)
        {
            return new LiteralExpressionNode(literalExpressionKind, token);
        }

        public static EqualsValueClauseNode EqualsValueClause()
        {
            return new EqualsValueClauseNode();
        }

        public static KSyntaxToken Literal(string stringLiteral)
        {
            return new KSyntaxToken(KSyntaxKind.StringLiteralToken, stringLiteral);
        }

        public static KSyntaxToken Literal(int numericLiteral)
        {
            return new KSyntaxToken(KSyntaxKind.NumericLiteralToken, numericLiteral.ToString());
        }

        public static AssignmentExpressionNode AssignmentExpression(KSyntaxKind simpleAssignmentExpression, ExpressionNode left, ExpressionNode right)
        {
            throw new NotImplementedException();
        }

        public static AssignmentExpressionNode AssignmentExpression(KSyntaxKind simpleAssignmentExpression)
        {
            return new AssignmentExpressionNode();
        }

        public static ForStatementNode ForStatement()
        {
            return new ForStatementNode();
        }

        public static BlockNode Block()
        {
            return new BlockNode()
                .WithOpenBrace(Token(KSyntaxKind.OpenBraceToken))
                .WithCloseBrace(Token(KSyntaxKind.CloseBraceToken));
        }

        public static BlockNode Block(params StatementNode[] statements)
        {
            return Block().WithStatements(statements);
        }

        public static BlockNode Block(IEnumerable<StatementNode> statements)
        {
            return Block(statements.ToArray());
        }

        public static LocalDeclarationStatementNode LocalDeclarationStatement()
        {
            return new LocalDeclarationStatementNode();
        }

        public static BinaryExpressionNode BinaryExpression(KSyntaxKind kind)
        {
            KSyntaxToken operatorToken;
            switch (kind)
            {
                case KSyntaxKind.LessThanExpression:
                    operatorToken = Token(KSyntaxKind.LessThanToken);
                    break;
                case KSyntaxKind.LessThanOrEqualExpression:
                    operatorToken = Token(KSyntaxKind.LessThanEqualsToken);
                    break;
                case KSyntaxKind.GreaterThanExpression:
                    operatorToken = Token(KSyntaxKind.GreaterThanToken);
                    break;
                case KSyntaxKind.GreaterThanOrEqualExpression:
                    operatorToken = Token(KSyntaxKind.GreaterThanEqualsToken);
                    break;
                case KSyntaxKind.AddExpression:
                    operatorToken = Token(KSyntaxKind.PlusToken);
                    break;
                case KSyntaxKind.SubtractExpression:
                    operatorToken = Token(KSyntaxKind.MinusToken);
                    break;
                default: throw new KeyNotFoundException($"Unable to find match for '{kind}' as a binary expression kind");
            }
            return new BinaryExpressionNode(kind).WithOperator(operatorToken);
        }
    }
}