using KerbalAnalysis.Nodes.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KerbalAnalysis.Nodes
{
    public abstract class KNode : INodeOrToken
    {
        public KNode Parent { get; set; }
        public KSyntaxKind Kind { get; set; }
        public abstract List<INodeOrToken> Children { get; }

        public IEnumerable<INodeOrToken> DescendantNodesAndTokens()
        {
            foreach (var child in Children)
            {
                yield return child;
                if (child is KNode nodeChild)
                {
                    var subChildren = nodeChild.DescendantNodesAndTokens();
                    foreach (var subChild in subChildren)
                    {
                        yield return subChild;
                    }
                }
            }
        }

        public IEnumerable<INodeOrToken> DescendantNodes() => DescendantNodesAndTokens().OfType<KNode>();
    }
}