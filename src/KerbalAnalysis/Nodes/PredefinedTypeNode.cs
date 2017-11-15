using KerbalAnalysis.Nodes.Abstract;
using System.Collections.Immutable;

namespace KerbalAnalysis.Nodes
{
    public class PredefinedTypeNode : TypeNode
    {
        public KSyntaxToken Keyword { get; set; }
        public override ImmutableList<INodeOrToken> Children => ImmutableList.Create<INodeOrToken>(Keyword);

        public PredefinedTypeNode()
        {
            Kind = KSyntaxKind.PredefinedType;
        }

        public PredefinedTypeNode WithKeyword(KSyntaxToken keyword)
        {
            keyword.Parent = this;
            Keyword = keyword;
            return this;
        }
    }
}