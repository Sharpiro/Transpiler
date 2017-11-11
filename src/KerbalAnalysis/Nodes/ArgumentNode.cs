namespace KerbalAnalysis.Nodes
{
    public class ArgumentNode : KNode
    {
        public ExpressionNode Expression { get; set; }

        public ArgumentNode()
        {
            Kind = KSyntaxKind.Argument;
        }

        public ArgumentNode WithExpression(ExpressionNode expression)
        {
            expression.Parent = this;
            Expression = expression;
            return this;
        }
    }
}