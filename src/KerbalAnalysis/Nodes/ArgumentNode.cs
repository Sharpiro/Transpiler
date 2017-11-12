using KerbalAnalysis.Nodes.Abstract;
using System.Collections.Generic;

namespace KerbalAnalysis.Nodes
{
    public class ArgumentNode : KNode
    {
        public ExpressionNode Expression { get; set; }
        public override List<INodeOrToken> Children => new List<INodeOrToken> { Expression };

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