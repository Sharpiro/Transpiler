namespace KerbalAnalysis.Nodes
{
    public class GlobalStatementNode : KNode
    {
        public StatementNode Statement { get; set; }

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