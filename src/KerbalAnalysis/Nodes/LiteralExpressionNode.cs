using System.Collections.Generic;
using KerbalAnalysis.Nodes.Abstract;

namespace KerbalAnalysis.Nodes
{
    public class LiteralExpressionNode : ExpressionNode
    {
        public KSyntaxToken Token { get; set; }
        public override List<INodeOrToken> Children => new List<INodeOrToken> { Token };

        public LiteralExpressionNode(KSyntaxKind kind, KSyntaxToken token)
        {
            Kind = kind;
            Token = token;
        }
    }
}