using KerbalAnalysis.Nodes.Abstract;
using System.Collections.Immutable;

namespace KerbalAnalysis.Nodes
{
    public class ArgumentNode : KNode
    {
        public ExpressionNode Expression { get; set; }
        public override ImmutableList<INodeOrToken> Children => ImmutableList.Create<INodeOrToken>(Expression);


        public ArgumentNode()
        {
            Kind = KSyntaxKind.Argument;
        }

        public ArgumentNode WithExpression(ExpressionNode expression)
        {
            expression.Parent = this;
            Expression = expression;
            return this;
        }
    }
}