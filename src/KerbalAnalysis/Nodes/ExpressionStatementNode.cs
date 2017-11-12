using KerbalAnalysis.Nodes.Abstract;
using System.Collections.Generic;

namespace KerbalAnalysis.Nodes
{
    public class ExpressionStatementNode : StatementNode
    {
        public ExpressionNode Expression { get; set; }
        public override List<INodeOrToken> Children => new List<INodeOrToken> { Expression };

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