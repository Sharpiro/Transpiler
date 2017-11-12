using KerbalAnalysis.Nodes.Abstract;
using System.Collections.Immutable;

namespace KerbalAnalysis.Nodes
{
    public class LiteralExpressionNode : ExpressionNode
    {
        public KSyntaxToken Token { get; set; }
        public override ImmutableList<INodeOrToken> Children => ImmutableList.Create<INodeOrToken>(Token);

        public LiteralExpressionNode(KSyntaxKind kind, KSyntaxToken token)
        {
            Kind = kind;
            Token = token;
        }
    }
}