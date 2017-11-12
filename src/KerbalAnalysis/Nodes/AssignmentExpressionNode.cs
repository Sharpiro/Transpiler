using System.Collections.Immutable;
using KerbalAnalysis.Nodes.Abstract;

namespace KerbalAnalysis.Nodes
{
    public class AssignmentExpressionNode : ExpressionNode
    {
        public KSyntaxToken Set { get; private set; }
        public ExpressionNode Left { get; private set; }
        public KSyntaxToken To { get; private set; }
        public ExpressionNode Right { get; private set; }

        public override ImmutableList<INodeOrToken> Children => ImmutableList.Create<INodeOrToken>(Set, Left, To, Right);

        //internal AssignmentExpressionNode(ExpressionNode left, KSyntaxToken operatorToken, ExpressionNode right)
        //{
        //    Kind = KSyntaxKind.SimpleAssignmentExpression;
        //}

        internal AssignmentExpressionNode()
        {
            Kind = KSyntaxKind.SimpleAssignmentExpression;
        }

        public AssignmentExpressionNode WithLeft(ExpressionNode expression)
        {
            expression.Parent = this;
            Left = expression;
            return this;
        }

        public AssignmentExpressionNode WithRight(ExpressionNode expression)
        {
            expression.Parent = this;
            Right = expression;
            return this;
        }

        public AssignmentExpressionNode WithSet(KSyntaxToken token)
        {
            token.Parent = this;
            Set = token;
            return this;
        }

        public AssignmentExpressionNode WithTo(KSyntaxToken token)
        {
            token.Parent = this;
            To = token;
            return this;
        }
    }
}