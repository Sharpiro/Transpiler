using System.Collections.Immutable;
using KerbalAnalysis.Nodes.Abstract;
using static KerbalAnalysis.Tools.ImmutableExtensions;

namespace KerbalAnalysis.Nodes
{
    public class BinaryExpressionNode : ExpressionNode
    {
        public ExpressionNode Left { get; set; }
        public KSyntaxToken OperatorToken { get; set; }
        public ExpressionNode Right { get; set; }
        public override ImmutableList<INodeOrToken> Children => CreateList(Left, OperatorToken, Right);

        internal BinaryExpressionNode(KSyntaxKind kind)
        {
            Kind = kind;
        }

        public BinaryExpressionNode WithLeft(ExpressionNode left)
        {
            left.Parent = this;
            Left = left;
            return this;
        }

        public BinaryExpressionNode WithOperator(KSyntaxToken operatorToken)
        {
            operatorToken.Parent = this;
            OperatorToken = operatorToken;
            return this;
        }

        public BinaryExpressionNode WithRight(ExpressionNode right)
        {
            right.Parent = this;
            Right = right;
            return this;
        }
    }
}