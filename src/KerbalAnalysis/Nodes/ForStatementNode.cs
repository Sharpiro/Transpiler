using System;
using System.Collections.Immutable;
using KerbalAnalysis.Nodes.Abstract;

namespace KerbalAnalysis.Nodes
{
    public class ForStatementNode : StatementNode
    {
        public KSyntaxToken FromKeyword { get; set; }

        public override ImmutableList<INodeOrToken> Children => throw new NotImplementedException();
    }
}