using System.Collections.Generic;
using KerbalAnalysis.Nodes.Abstract;

namespace KerbalAnalysis.Nodes
{
    public class LiteralExpressionNode : ExpressionNode
    {
        public SyntaxToken Token { get; set; }
        public override List<INodeOrToken> Children => new List<INodeOrToken> { Token };

        public LiteralExpressionNode(KSyntaxKind kind, SyntaxToken token)
        {
            Kind = kind;
            Token = token;
        }
    }
}