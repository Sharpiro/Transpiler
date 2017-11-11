namespace KerbalAnalysis.Nodes
{
    public class LiteralExpressionNode : ExpressionNode
    {
        public SyntaxToken Token { get; set; }

        public LiteralExpressionNode(KSyntaxKind kind, SyntaxToken token)
        {
            Kind = kind;
            Token = token;
        }
    }
}