using System.Collections.Generic;
using KerbalAnalysis.Nodes.Abstract;
using System.Linq;

namespace KerbalAnalysis.Nodes
{
    public class ArgumentListNode : KNode
    {
        public List<ArgumentNode> Arguments { get; set; } = new List<ArgumentNode>();
        public override List<INodeOrToken> Children => Arguments.Cast<INodeOrToken>().ToList();

        public ArgumentListNode()
        {
            Kind = KSyntaxKind.ArgumentList;
        }

        public ArgumentListNode AddArgument(ArgumentNode argument)
        {
            argument.Parent = this;
            Arguments.Add(argument);
            return this;
        }
    }
}