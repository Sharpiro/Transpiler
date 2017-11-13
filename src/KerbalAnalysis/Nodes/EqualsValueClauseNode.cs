using System.Collections.Immutable;
using KerbalAnalysis.Nodes.Abstract;

namespace KerbalAnalysis.Nodes
{
    public class EqualsValueClauseNode : KNode
    {
        public KSyntaxToken Is { get; set; }
        public ExpressionNode Value { get; set; }

        public override ImmutableList<INodeOrToken> Children => throw new System.NotImplementedException();
    }
}