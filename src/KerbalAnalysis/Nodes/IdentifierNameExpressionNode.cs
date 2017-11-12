using KerbalAnalysis.Nodes.Abstract;
using System.Collections.Immutable;

namespace KerbalAnalysis.Nodes
{
    public class IdentifierNameExpressionNode : ExpressionNode
    {
        public KSyntaxToken Identifier { get; set; }
        public override ImmutableList<INodeOrToken> Children => ImmutableList.Create<INodeOrToken>(Identifier);

        public IdentifierNameExpressionNode(KSyntaxToken identifier)
        {
            Kind = KSyntaxKind.IdentifierNameExpression;
            Identifier = identifier;
        }
    }
}