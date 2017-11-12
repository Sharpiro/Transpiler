using KerbalAnalysis.Nodes.Abstract;
using System.Collections.Generic;

namespace KerbalAnalysis.Nodes
{
    public class GlobalStatementNode : MemberDeclarationNode
    {
        public StatementNode Statement { get; set; }
        public override List<INodeOrToken> Children => new List<INodeOrToken> { Statement };

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