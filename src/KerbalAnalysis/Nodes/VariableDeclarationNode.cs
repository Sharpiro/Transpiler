using System.Collections.Immutable;
using KerbalAnalysis.Nodes.Abstract;

namespace KerbalAnalysis.Nodes
{
    public class VariableDeclarationNode : KNode
    {
        public TypeNode Type { get; set; }
        public VariableDeclaratorNode Declarator { get; set; }
        public override ImmutableList<INodeOrToken> Children => ImmutableList.Create<INodeOrToken>(Type, Declarator);

        public VariableDeclarationNode()
        {
            Kind = KSyntaxKind.VariableDeclaration;
        }

        public VariableDeclarationNode WithType(TypeNode type)
        {
            type.Parent = this;
            Type = type;
            return this;
        }

        public VariableDeclarationNode WithVariableDeclarator(VariableDeclaratorNode declarator)
        {
            declarator.Parent = this;
            Declarator = declarator;
            return this;
        }
    }
}