using KerbalAnalysis.Nodes.Abstract;
using System.Collections.Generic;

namespace KerbalAnalysis.Nodes
{
    public class IdentifierNameExpressionNode : ExpressionNode
    {
        public KSyntaxToken Identifier { get; set; }
        public override List<INodeOrToken> Children => new List<INodeOrToken> { Identifier };

        public IdentifierNameExpressionNode(KSyntaxToken identifier)
        {
            Kind = KSyntaxKind.IdentifierNameExpression;
            Identifier = identifier;
        }
    }
}