using KerbalAnalysis.Nodes;
using KerbalAnalysis.Nodes.Abstract;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Immutable;

namespace KerbalAnalysis
{
    public class KSourceWriter
    {
        private const string Space = " ";
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
            _builder.Append(Space);
        }

        private void WriteDeclarator(VariableDeclaratorNode declarator)
        {
            _builder.Append(declarator.Identifier);
            _builder.Append(Space);
            WriteInitializer(declarator.Initializer);
        }

        private void WriteInitializer(EqualsValueClauseNode initializer)
        {
            _builder.Append(initializer.IsToken);
            _builder.Append(Space);
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
                case KSyntaxKind.ForStatement:
                    WriteForStatement(statement as ForStatementNode);
                    break;
                case KSyntaxKind.Block:
                    WriteBlock(statement as BlockNode);
                    break;
                case KSyntaxKind.LocalDeclarationStatement:
                    WriteLocalDeclarationStatement(statement as LocalDeclarationStatementNode);
                    break;
                default:
                    throw new KeyNotFoundException($"couldn't find statement with name '{statement.Kind}'");
            }
        }

        private void WriteLocalDeclarationStatement(LocalDeclarationStatementNode localDeclarationStatementNode)
        {
            WriteDeclaration(localDeclarationStatementNode.Declaration);
            _builder.Append(localDeclarationStatementNode.PeriodToken);
        }

        private void WriteForStatement(ForStatementNode forStatementNode)
        {
            _builder.Append(forStatementNode.FromKeyword);
            _builder.Append(Space);
            WriteBlock(forStatementNode.DeclarationBlock);
            _builder.Append(Space);
            _builder.Append(forStatementNode.UntilKeyword);
            _builder.Append(Space);
            WriteExpression(forStatementNode.Condition);
            _builder.Append(Space);
            _builder.Append(forStatementNode.StepKeyword);
            _builder.Append(Space);
            WriteBlock(forStatementNode.IncrementBlock);
            _builder.Append(Space);
            _builder.Append(forStatementNode.DoKeyword);
            _builder.Append(Space);
            WriteStatement(forStatementNode.Statement);
        }

        private void WriteBlock(BlockNode declarationBlock)
        {
            _builder.Append(declarationBlock.OpenBraceToken);
            WriteStatements(declarationBlock.Statements);
            _builder.Append(declarationBlock.CloseBraceToken);
        }

        private void WriteStatements(ImmutableList<StatementNode> statements)
        {
            foreach (var statement in statements)
            {
                WriteStatement(statement);
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
                case KSyntaxKind.AddExpression:
                case KSyntaxKind.LessThanExpression:
                case KSyntaxKind.LessThanOrEqualExpression:
                case KSyntaxKind.GreaterThanExpression:
                case KSyntaxKind.GreaterThanOrEqualExpression:
                    WriteBinaryExpression(expression as BinaryExpressionNode);
                    break;
                default:
                    throw new KeyNotFoundException($"couldn't find expression with kind '{expression.Kind}'");
            }
        }

        private void WriteBinaryExpression(BinaryExpressionNode binaryExpressionNode)
        {
            WriteExpression(binaryExpressionNode.Left);
            _builder.Append(Space);
            _builder.Append(binaryExpressionNode.OperatorToken);
            _builder.Append(Space);
            WriteExpression(binaryExpressionNode.Right);
        }

        private void WriteSimpleAssignmentExpression(AssignmentExpressionNode assignmentExpression)
        {
            _builder.Append($"{assignmentExpression.SetKeyword} ");
            WriteExpression(assignmentExpression.Left);
            _builder.Append(Space);
            _builder.Append($"{assignmentExpression.ToKeyword} ");
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