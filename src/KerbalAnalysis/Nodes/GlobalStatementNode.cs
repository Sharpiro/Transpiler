using KerbalAnalysis.Nodes.Abstract;
using System.Collections.Immutable;

namespace KerbalAnalysis.Nodes
{
    public class GlobalStatementNode : MemberDeclarationNode
    {
        public StatementNode Statement { get; set; }
        public override ImmutableList<INodeOrToken> Children => ImmutableList.Create<INodeOrToken>(Statement);

        public GlobalStatementNode()
        {
            Kind = KSyntaxKind.GlobalStatement;
        }

        public GlobalStatementNode WithStatement(StatementNode statement)
        {
            Statement = statement;
            statement.Parent = this;
            return this;
        }
    }
}