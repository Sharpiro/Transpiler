using KerbalAnalysis.Nodes;
using KerbalAnalysis.Nodes.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace KerbalAnalysis
{
    public class KSourceWriter
    {
        private StringBuilder _builder = new StringBuilder();

        public string GetSourceCode(CompilationUnitNode compilation)
        {
            _builder.Clear();
            WriteGlobalStatements(compilation.Members);
            return _builder.ToString();
        }

        private void WriteGlobalStatements(List<MemberDeclarationNode> globalStatements)
        {
            foreach (GlobalStatementNode globalStatement in globalStatements)
            {
                switch (globalStatement.Statement.Kind)
                {
                    case KSyntaxKind.ExpressionStatement:
                        WriteExpressionStatement(globalStatement.Statement);
                        break;
                    default:
                        throw new KeyNotFoundException($"couldn't find statement with name '{globalStatement.Kind()}'");
                }
                //compilationUnit.AddMember(kGlobalStatement);
            }
        }

        private void WriteExpressionStatement(ExpressionStatementNode expressionStatement)
        {
            WriteExpression(expressionStatement.Expression);
            _builder.Append(expressionStatement.Period);
        }

        private void WriteExpression(ExpressionNode expression)
        {
            switch (expression.Kind)
            {
                case KSyntaxKind.InvocationExpression:
                    WriteInvocationExpression(expression as InvocationExpressionNode);
                    break;
                case KSyntaxKind.StringLiteralExpression:
                    WriteLiteralExpression(expression as LiteralExpressionNode);
                    break;
                case KSyntaxKind.IdentifierNameExpression:
                    WriteIdentifierNameExpression(expression as IdentifierNameExpressionNode);
                    break;
                default:
                    throw new KeyNotFoundException($"couldn't find expression with kind '{expression.Kind()}'");
            }
        }

        private void WriteLiteralExpression(LiteralExpressionNode literalExpression)
        {
            _builder.Append(literalExpression.Token);
        }

        private void WriteInvocationExpression(InvocationExpressionNode expression)
        {
            WriteExpression(expression.Expression);
            WriteArgumentList(expression.ArgumentList);
        }

        private void WriteIdentifierNameExpression(IdentifierNameExpressionNode identifierNameNode)
        {
            _builder.Append(identifierNameNode.Identifier);
        }

        private void WriteArgumentList(ArgumentListNode argumentList)
        {
            //var kArgumentList = KSyntaxFactory.ArgumentList();
            //invocationExpression.WithArgumentList(kArgumentList);

            foreach (var argument in argumentList.Arguments)
            {
                WriteArgument(argument);
            }
        }

        private void WriteArgument(ArgumentNode argument)
        {
            var kArgument = KSyntaxFactory.Argument();
            WriteExpression(argument.Expression, kArgument);
            argumentList.AddArgument(kArgument);
        }
    }
}