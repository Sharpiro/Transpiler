using System.Collections.Immutable;
using KerbalAnalysis.Nodes.Abstract;

namespace KerbalAnalysis.Nodes
{
    public class EqualsValueClauseNode : KNode
    {
        public KSyntaxToken IsToken { get; set; }
        public ExpressionNode Value { get; set; }
        public override ImmutableList<INodeOrToken> Children => ImmutableList.Create<INodeOrToken>(IsToken, Value);

        internal EqualsValueClauseNode()
        {
            Kind = KSyntaxKind.EqualsValueClause;
        }

        public EqualsValueClauseNode WithIsKeyword(KSyntaxToken isToken)
        {
            isToken.Parent = this;
            IsToken = isToken;
            return this;
        }

        public EqualsValueClauseNode WithValue(ExpressionNode value)
        {
            value.Parent = this;
            Value = value;
            return this;
        }
    }
}