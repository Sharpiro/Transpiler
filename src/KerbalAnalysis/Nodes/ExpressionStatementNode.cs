using KerbalAnalysis.Nodes.Abstract;
using System.Collections.Immutable;

namespace KerbalAnalysis.Nodes
{
    public class ExpressionStatementNode : StatementNode
    {
        public ExpressionNode Expression { get; set; }
        public KSyntaxToken Period { get; } = KSyntaxFactory.Token(KSyntaxKind.Period);

        public override ImmutableList<INodeOrToken> Children => ImmutableList.Create<INodeOrToken>(Expression, Period);

        public ExpressionStatementNode()
        {
            Kind = KSyntaxKind.ExpressionStatement;
        }

        public ExpressionStatementNode WithExpression(ExpressionNode expression)
        {
            Expression = expression;
            expression.Parent = this;
            return this;
        }
    }
}