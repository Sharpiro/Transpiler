namespace KerbalAnalysis.Nodes
{
    public class IdentifierNameExpressionNode : ExpressionNode
    {
        public SyntaxToken Identifier { get; set; }

        //public IdentifierNameExpressionNode()
        //{
        //    Kind = SyntaxKind.IdentifierNameExpression;
        //}

        public IdentifierNameExpressionNode(SyntaxToken identifier)
        {
            Kind = KSyntaxKind.IdentifierNameExpression;
            Identifier = identifier;
        }
    }
}