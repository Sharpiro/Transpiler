using KerbalAnalysis.Nodes.Abstract;
using System.Collections.Immutable;

namespace KerbalAnalysis.Tools
{
    public static class ImmutableExtensions
    {
        public static ImmutableList<INodeOrToken> CreateList(params INodeOrToken[] items)
        {
            return ImmutableList.Create(items);
        }
    }
}