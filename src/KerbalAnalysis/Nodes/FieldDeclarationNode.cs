using KerbalAnalysis.Nodes.Abstract;
using System.Collections.Immutable;

namespace KerbalAnalysis.Nodes
{
    public class FieldDeclarationNode : MemberDeclarationNode
    {
        public VariableDeclarationNode Declaration { get; set; }
        public KSyntaxToken PeriodToken { get; set; }
        public override ImmutableList<INodeOrToken> Children => ImmutableList.Create<INodeOrToken>(Declaration, PeriodToken);

        internal FieldDeclarationNode()
        {
            Kind = KSyntaxKind.FieldDeclaration;
        }

        public FieldDeclarationNode WithVariableDeclaration(VariableDeclarationNode declaration)
        {
            declaration.Parent = this;
            Declaration = declaration;
            return this;
        }

        public FieldDeclarationNode WithPeriod(KSyntaxToken period)
        {
            period.Parent = this;
            PeriodToken = period;
            return this;
        }
    }
}