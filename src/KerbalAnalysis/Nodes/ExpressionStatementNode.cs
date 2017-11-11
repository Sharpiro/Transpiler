namespace KerbalAnalysis.Nodes
{
    public class ExpressionStatementNode : StatementNode
    {
        public ExpressionNode Expression { get; set; }

        public ExpressionStatementNode()
        {
            Kind = KSyntaxKind.ExpressionStatement;
        }

        public ExpressionStatementNode WithExpression(ExpressionNode expression)
        {
            Expression = expression;
            expression.Parent = this;
            return this;
        }
    }
}