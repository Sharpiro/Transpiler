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
            WriteMembers(compilation.Members);
            return _builder.ToString();
        }

        private void WriteMembers(IReadOnlyList<MemberDeclarationNode> members)
        {
            var counter = 0;
            foreach (var member in members)
            {
                if (counter != 0) _builder.AppendLine();
                WriteMember(member);
                counter++;
            }
        }

        private void WriteMember(MemberDeclarationNode member)
        {
            switch (member.Kind)
            {
                case KSyntaxKind.GlobalStatement:
                    WriteGlobalStatement(member as GlobalStatementNode);
                    break;
                case KSyntaxKind.FieldDeclaration:
                    WriteFieldDeclaration(member as FieldDeclarationNode);
                    break;
                default: throw new KeyNotFoundException($"couldn't find member with name '{member.Kind}'");
            }
        }

        private void WriteFieldDeclaration(FieldDeclarationNode fieldDeclarationNode)
        {
            WriteDeclaration(fieldDeclarationNode.Declaration);
            _builder.Append(fieldDeclarationNode.PeriodToken);
        }

        private void WriteDeclaration(VariableDeclarationNode declaration)
        {
            WriteType(declaration.Type);
            WriteDeclarator(declaration.Declarator);
        }

        private void WriteType(TypeNode type)
        {
            switch (type.Kind)
            {
                case KSyntaxKind.PredefinedType:
                    WritePredefinedType(type as PredefinedTypeNode);
                    break;
                default: throw new KeyNotFoundException($"couldn't find type node with name '{type.Kind}'");
            }
        }

        private void WritePredefinedType(PredefinedTypeNode type)
        {
            _builder.Append(type.Keyword);
            _builder.Append(" ");
        }

        private void WriteDeclarator(VariableDeclaratorNode declarator)
        {
            _builder.Append(declarator.Identifier);
            _builder.Append(" ");
            WriteInitializer(declarator.Initializer);
        }

        private void WriteInitializer(EqualsValueClauseNode initializer)
        {
            _builder.Append(initializer.IsToken);
            _builder.Append(" ");
            WriteExpression(initializer.Value);
        }

        private void WriteGlobalStatement(GlobalStatementNode globalStatement)
        {
            WriteStatement(globalStatement.Statement);
        }

        private void WriteStatement(StatementNode statement)
        {
            switch (statement.Kind)
            {
                case KSyntaxKind.ExpressionStatement:
                    WriteExpressionStatement(statement as ExpressionStatementNode);
                    break;
                default:
                    throw new KeyNotFoundException($"couldn't find statement with name '{statement.Kind}'");
            }
        }

        private void WriteExpressionStatement(ExpressionStatementNode expressionStatement)
        {
            WriteExpression(expressionStatement.Expression);
            _builder.Append(expressionStatement.PeriodToken);
        }

        private void WriteExpression(ExpressionNode expression)
        {
            switch (expression.Kind)
            {
                case KSyntaxKind.InvocationExpression:
                    WriteInvocationExpression(expression as InvocationExpressionNode);
                    break;
                case KSyntaxKind.NumericLiteralExpression:
                case KSyntaxKind.StringLiteralExpression:
                    WriteLiteralExpression(expression as LiteralExpressionNode);
                    break;
                case KSyntaxKind.IdentifierNameExpression:
                    WriteIdentifierNameExpression(expression as IdentifierNameExpressionNode);
                    break;
                case KSyntaxKind.SimpleAssignmentExpression:
                    WriteSimpleAssignmentExpression(expression as AssignmentExpressionNode);
                    break;
                default:
                    throw new KeyNotFoundException($"couldn't find expression with kind '{expression.Kind}'");
            }
        }

        private void WriteSimpleAssignmentExpression(AssignmentExpressionNode assignmentExpression)
        {
            _builder.Append($"{assignmentExpression.Set} ");
            WriteExpression(assignmentExpression.Left);
            _builder.Append(" ");
            _builder.Append($"{assignmentExpression.To} ");
            WriteExpression(assignmentExpression.Right);
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