using KerbalAnalysis.Nodes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using KerbalAnalysis.Nodes.Abstract;
using System;

namespace KerbalAnalysis
{
    public class KTreeBuilder
    {
        public CompilationUnitNode CreateCompilation(CompilationUnitSyntax compilation)
        {
            var kCompilation = new CompilationUnitNode();
            var kMembers = ParseMembers(compilation.Members.ToList());
            kCompilation.WithMembers(kMembers);
            return kCompilation;
        }

        private IEnumerable<MemberDeclarationNode> ParseMembers(List<MemberDeclarationSyntax> members)
        {
            foreach (var member in members)
            {
                yield return ParseMember(member);
            }
        }

        private MemberDeclarationNode ParseMember(MemberDeclarationSyntax member)
        {
            MemberDeclarationNode memberDeclarationNode;
            switch (member.Kind())
            {
                case SyntaxKind.GlobalStatement:
                    memberDeclarationNode = ParseGlobalStatement(member as GlobalStatementSyntax);
                    break;
                case SyntaxKind.FieldDeclaration:
                    memberDeclarationNode = ParseFieldDeclaration(member as FieldDeclarationSyntax);
                    break;
                default:
                    throw new KeyNotFoundException($"couldn't find member with name '{member.Kind()}'");
            }
            return memberDeclarationNode;
        }

        private FieldDeclarationNode ParseFieldDeclaration(FieldDeclarationSyntax fieldDeclaration)
        {
            var declaration = ParseDeclaration(fieldDeclaration.Declaration);
            var kFieldDeclaration = KSyntaxFactory.FieldDeclaration()
                .WithVariableDeclaration(declaration)
                .WithPeriod(KSyntaxFactory.Token(KSyntaxKind.Period));
            return kFieldDeclaration;
        }

        private VariableDeclarationNode ParseDeclaration(VariableDeclarationSyntax declaration)
        {
            var type = ParseType(declaration.Type);
            var variableDeclarator = ParseDeclarator(declaration.Variables.First());
            var kDeclaration = KSyntaxFactory.VariableDeclaration()
                .WithType(type)
                .WithVariableDeclarator(variableDeclarator);
            return kDeclaration;
        }

        private TypeNode ParseType(TypeSyntax type)
        {
            TypeNode kType;
            switch (type.Kind())
            {
                case SyntaxKind.PredefinedType:
                    kType = ParsePredefinedType(type as PredefinedTypeSyntax);
                    break;
                default:
                    throw new KeyNotFoundException($"couldn't find member with name '{type.Kind()}'");
            }
            return kType;
        }

        private PredefinedTypeNode ParsePredefinedType(PredefinedTypeSyntax type)
        {
            var keyword = KSyntaxFactory.Token(KSyntaxKind.LocalKeyword);
            var kPredefinedType = KSyntaxFactory.PredefinedType(keyword);
            return kPredefinedType;
        }

        private VariableDeclaratorNode ParseDeclarator(VariableDeclaratorSyntax variableDeclaratorSyntax)
        {
            var identifier = KSyntaxFactory.Identifier(variableDeclaratorSyntax.Identifier.Text);
            var initializer = ParseInitializer(variableDeclaratorSyntax.Initializer);
            var kDeclarator = KSyntaxFactory.VariableDeclarator()
                .WithIdentifier(identifier)
                .WithInitializer(initializer);
            return kDeclarator;
        }

        private EqualsValueClauseNode ParseInitializer(EqualsValueClauseSyntax initializer)
        {
            var isToken = KSyntaxFactory.Token(KSyntaxKind.IsKeyword);
            var value = ParseExpression(initializer.Value);
            var kInitializer = KSyntaxFactory.EqualsValueClause()
                .WithIsKeyword(isToken)
                .WithValue(value);
            return kInitializer;
        }

        private GlobalStatementNode ParseGlobalStatement(GlobalStatementSyntax globalStatement)
        {
            var kGlobalStatement = new GlobalStatementNode();
            var statement = ParseStatement(globalStatement.Statement);
            kGlobalStatement.WithStatement(statement);
            return kGlobalStatement;
        }

        private StatementNode ParseStatement(StatementSyntax statement)
        {
            ExpressionStatementNode expressionStatement;
            switch (statement.Kind())
            {
                //case SyntaxKind.ForStatement:
                //    expressionStatement = ParseForStatement(globalStatement.Statement as ForStatementSyntax);
                //    break;
                case SyntaxKind.ExpressionStatement:
                    expressionStatement = ParseExpressionStatement(statement as ExpressionStatementSyntax);
                    break;
                default:
                    throw new KeyNotFoundException($"couldn't find statement with name '{statement.Kind()}'");
            }
            return expressionStatement;
        }

        //private ForStatementNode ParseForStatement(ForStatementSyntax forStatement)
        //{
        //    //var localVarName = "countdown";
        //    //var initValue = 10;
        //    //var conditionalExpression = "countdown = 0";
        //    //var statement = $"FROM {{local {localVarName} is {initValue}.}} {conditionalExpression} STEP {{SET {localVarName} to {localVarName} - 1.}} DO";

        //    throw new NotImplementedException();
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
            var value = literalExpression.Token.Text;
            var kLiteralExpression = KSyntaxFactory.LiteralExpression((KSyntaxKind)literalExpression.Kind(), KSyntaxFactory.Literal(value));
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