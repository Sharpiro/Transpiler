using KerbalAnalysis.Nodes.Abstract;

namespace KerbalAnalysis.Nodes
{
    public abstract class KNode : IParent
    {
        public KNode Parent { get; set; }
        public KSyntaxKind Kind { get; set; }
    }
}