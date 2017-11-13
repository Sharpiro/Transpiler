using KerbalAnalysis.Nodes.Abstract;
using System.Collections.Immutable;

namespace KerbalAnalysis.Nodes
{
    public class FieldDeclarationNode : MemberDeclarationNode
    {
        public VariableDeclarationNode Declaration { get; set; }
        public override ImmutableList<INodeOrToken> Children => ImmutableList.Create<INodeOrToken>(Declaration);

        internal FieldDeclarationNode()
        {

        }
    }
}