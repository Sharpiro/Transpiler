using KerbalAnalysis.Nodes;
using KerbalAnalysis.Nodes.Abstract;

namespace KerbalAnalysis
{
    public class SyntaxToken : IParent
    {
        public KSyntaxKind Kind { get; set; }
        public string Text { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public KNode Parent { get; set; }
    }
}