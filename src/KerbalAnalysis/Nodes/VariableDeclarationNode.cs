using System.Collections.Immutable;
using KerbalAnalysis.Nodes.Abstract;

namespace KerbalAnalysis.Nodes
{
    public class VariableDeclarationNode : KNode
    {
        public VariableDeclaratorNode Variable { get; set; }
        public override ImmutableList<INodeOrToken> Children => throw new System.NotImplementedException();
    }
}