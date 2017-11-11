namespace KerbalAnalysis.Nodes
{
    public class InvocationExpressionNode : ExpressionNode
    {
        public ArgumentListNode ArgumentList { get; set; }
        public ExpressionNode Expression { get; set; }

        public InvocationExpressionNode()
        {
            Kind = KSyntaxKind.InvocationExpression;
        }

        public InvocationExpressionNode WithArgumentList(ArgumentListNode kArgumentList)
        {
            ArgumentList = kArgumentList;
            kArgumentList.Parent = this;
            return this;
        }
    }
}