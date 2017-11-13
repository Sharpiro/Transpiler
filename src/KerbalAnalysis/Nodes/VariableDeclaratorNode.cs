using System.Collections.Immutable;
using KerbalAnalysis.Nodes.Abstract;

namespace KerbalAnalysis.Nodes
{
    public class VariableDeclaratorNode : KNode
    {
        public KSyntaxToken Identifier { get; set; }
        public EqualsValueClauseNode Initializer { get; set; }
        public override ImmutableList<INodeOrToken> Children => throw new System.NotImplementedException();
    }
}