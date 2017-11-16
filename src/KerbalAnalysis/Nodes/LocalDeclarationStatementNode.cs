using System.Collections.Immutable;
using KerbalAnalysis.Nodes.Abstract;
using static KerbalAnalysis.Tools.ImmutableExtensions;

namespace KerbalAnalysis.Nodes
{
    public class LocalDeclarationStatementNode : StatementNode
    {
        public VariableDeclarationNode Declaration { get; set; }
        public KSyntaxToken PeriodToken { get; set; }
        public override ImmutableList<INodeOrToken> Children => CreateList(Declaration, PeriodToken);

        public LocalDeclarationStatementNode()
        {
            Kind = KSyntaxKind.LocalDeclarationStatement;
        }

        public LocalDeclarationStatementNode WithDeclaration(VariableDeclarationNode declaration)
        {
            declaration.Parent = this;
            Declaration = declaration;
            return this;
        }

        public LocalDeclarationStatementNode WithPeriod(KSyntaxToken periodToken)
        {
            periodToken.Parent = this;
            PeriodToken = periodToken;
            return this;
        }
    }
}