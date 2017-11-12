using System;
using System.Collections.Generic;
using KerbalAnalysis.Nodes.Abstract;

namespace KerbalAnalysis.Nodes
{
    public class InvocationExpressionNode : ExpressionNode
    {
        public ExpressionNode Expression { get; set; }
        public ArgumentListNode ArgumentList { get; set; }
        public override List<INodeOrToken> Children => new List<INodeOrToken> { Expression, ArgumentList };

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

        public InvocationExpressionNode WithExpression(ExpressionNode kIdentifierExpression)
        {
            kIdentifierExpression.Parent = this;
            Expression = kIdentifierExpression;
            return this;
        }
    }
}