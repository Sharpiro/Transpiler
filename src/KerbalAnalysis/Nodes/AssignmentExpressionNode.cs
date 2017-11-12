using System.Collections.Immutable;
using KerbalAnalysis.Nodes.Abstract;

namespace KerbalAnalysis.Nodes
{
    public class AssignmentExpressionNode : ExpressionNode
    {
        public KSyntaxToken Set { get; set; }
        public ExpressionNode Left { get; }
        public KSyntaxToken To { get; set; }
        public ExpressionNode Right { get; }

        public override ImmutableList<INodeOrToken> Children => ImmutableList.Create<INodeOrToken>(Set, Left, To, Right);

        //internal AssignmentExpressionNode(ExpressionNode left, KSyntaxToken operatorToken, ExpressionNode right)
        //{
        //    Kind = KSyntaxKind.SimpleAssignmentExpression;
        //}

        internal AssignmentExpressionNode()
        {
            Kind = KSyntaxKind.SimpleAssignmentExpression;
        }
    }
}