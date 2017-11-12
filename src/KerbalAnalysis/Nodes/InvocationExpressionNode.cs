using KerbalAnalysis.Nodes.Abstract;
using System.Collections.Immutable;

namespace KerbalAnalysis.Nodes
{
    public class InvocationExpressionNode : ExpressionNode
    {
        public ExpressionNode Expression { get; set; }
        public ArgumentListNode ArgumentList { get; set; }
        public override ImmutableList<INodeOrToken> Children => ImmutableList.Create<INodeOrToken>(Expression, ArgumentList);

        public InvocationExpressionNode()
        {
            Kind = KSyntaxKind.InvocationExpression;
        }

        public InvocationExpressionNode WithArgumentList(ArgumentListNode kArgumentList)
        {
            ArgumentList = kArgumentList;
            kArgumentList.Parent = this;
            return this;
        }

        public InvocationExpressionNode WithExpression(ExpressionNode kIdentifierExpression)
        {
            kIdentifierExpression.Parent = this;
            Expression = kIdentifierExpression;
            return this;
        }
    }
}