using KerbalAnalysis.Nodes.Abstract;
using System.Collections.Immutable;

namespace KerbalAnalysis.Nodes
{
    public class ExpressionStatementNode : StatementNode
    {
        public ExpressionNode Expression { get; private set; }
        public KSyntaxToken PeriodToken { get; private set; }

        public override ImmutableList<INodeOrToken> Children => ImmutableList.Create<INodeOrToken>(Expression, PeriodToken);

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

        public ExpressionStatementNode WithPeriod(KSyntaxToken periodToken)
        {
            periodToken.Parent = this;
            PeriodToken = periodToken;
            return this;
        }
    }
}