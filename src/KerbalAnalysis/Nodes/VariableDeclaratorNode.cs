using System.Collections.Immutable;
using KerbalAnalysis.Nodes.Abstract;

namespace KerbalAnalysis.Nodes
{
    public class VariableDeclaratorNode : KNode
    {
        public KSyntaxToken Identifier { get; set; }
        public EqualsValueClauseNode Initializer { get; set; }
        public override ImmutableList<INodeOrToken> Children => ImmutableList.Create<INodeOrToken>(Identifier, Initializer);

        internal VariableDeclaratorNode()
        {
            Kind = KSyntaxKind.VariableDeclarator;
        }

        public VariableDeclaratorNode WithIdentifier(KSyntaxToken identifier)
        {
            identifier.Parent = this;
            Identifier = identifier;
            return this;
        }

        public VariableDeclaratorNode WithInitializer(EqualsValueClauseNode initializer)
        {
            initializer.Parent = this;
            Initializer = initializer;
            return this;
        }
    }
}