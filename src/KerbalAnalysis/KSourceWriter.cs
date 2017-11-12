using KerbalAnalysis.Nodes;
using KerbalAnalysis.Nodes.Abstract;
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

        private void WriteGlobalStatements(IReadOnlyList<MemberDeclarationNode> globalStatements)
        {
            var counter = 0;
            foreach (GlobalStatementNode globalStatement in globalStatements)
            {
                if (counter != 0) _builder.AppendLine();
                switch (globalStatement.Statement.Kind)
                {
                    case KSyntaxKind.ExpressionStatement:
                        WriteExpressionStatement(globalStatement.Statement as ExpressionStatementNode);
                        break;
                    default:
                        throw new KeyNotFoundException($"couldn't find statement with name '{globalStatement.Kind}'");
                }
                counter++;
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
                    throw new KeyNotFoundException($"couldn't find expression with kind '{expression.Kind}'");
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
            if (argumentList.Count > 0)
                _builder.Append(argumentList.OpenParenToken.Text);

            foreach (var argument in argumentList.Arguments)
            {
                WriteArgument(argument);
            }

            if (argumentList.Count > 0)
                _builder.Append(argumentList.CloseParenToken.Text);
        }

        private void WriteArgument(ArgumentNode argument)
        {
            WriteExpression(argument.Expression);
        }
    }
}