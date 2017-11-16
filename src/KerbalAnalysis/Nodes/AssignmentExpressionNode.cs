using System.Collections.Immutable;
using KerbalAnalysis.Nodes.Abstract;

namespace KerbalAnalysis.Nodes
{
    public class AssignmentExpressionNode : ExpressionNode
    {
        public KSyntaxToken SetKeyword { get; private set; }
        public ExpressionNode Left { get; private set; }
        public KSyntaxToken ToKeyword { get; private set; }
        public ExpressionNode Right { get; private set; }

        public override ImmutableList<INodeOrToken> Children => ImmutableList.Create<INodeOrToken>(SetKeyword, Left, ToKeyword, Right);

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

        public AssignmentExpressionNode WithSetKeyword(KSyntaxToken token)
        {
            token.Parent = this;
            SetKeyword = token;
            return this;
        }

        public AssignmentExpressionNode WithToKeyword(KSyntaxToken token)
        {
            token.Parent = this;
            ToKeyword = token;
            return this;
        }
    }
}